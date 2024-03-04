using AutoMapper;
using Framework.Core.Event;
using Framework.Core.Event.External;
using LookUp.Domain.Dtos;
using LookUp.Domain.Entities;
using LookUp.Domain.EventEnvelopes.Attributes;
using LookUp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookUp.Domain.Services
{
        public interface ILookUpService
        {
            Task<IEnumerable<AttributesDto>> GetAllAttributes();
            Task<AttributesDto> AddAttributes(AttributesDto dto);
        }

        public class AttributesServices : ILookUpService
        {
            private ILookUpReposiotories _repository;
            private readonly IMapper _mapper;
            private readonly IExternalEventProducer _externalEventProducer;
            public AttributesServices(ILookUpReposiotories reposiotory, IExternalEventProducer externalEventProducer, IMapper mapper)
            {
                _repository = reposiotory;
                _mapper = mapper;
                _externalEventProducer = externalEventProducer;
            }
            public async Task<AttributesDto> AddAttributes(AttributesDto dto)
            {
                var dtoToEntity = _mapper.Map<AttributesEntity>(dto);
                var entity = await _repository.Add(dtoToEntity);
                var result = await _repository.SaveChangeAsyns();
                if(result > 0)
                {
                    var externalEvent = new EventEnvelope<AttributeCreated>(
                        AttributeCreated.Created(
                        id: entity.Id,
                        unit: entity.Unit,
                        type: entity.Type,
                        status: entity.Status
                        ));
                    await _externalEventProducer.Publish(externalEvent, new CancellationToken());
                    return _mapper.Map<AttributesDto>(entity);
                }
                return new AttributesDto();
            }

            public async Task<IEnumerable<AttributesDto>> GetAllAttributes()
            {
                return _mapper.Map<IEnumerable<AttributesDto>>(await _repository.GetAll());
            }
        }
    }