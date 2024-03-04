﻿using AutoMapper;
using Framework.Core.Event;
using Framework.Core.Event.External;
using LookUp.Domain.Dtos;
using LookUp.Domain.Entities;
using LookUp.Domain.EventEnvelopes.Attributes;
using LookUp.Domain.Repositories;

namespace LookUp.Domain.Services
{
    public interface IAttributeService
    {
        Task<IEnumerable<AttributeDto>> GetAllAttributes();
        Task<AttributeDto> AddAttribute(AttributeDto dto);
        Task<AttributeDto> UpdateAttributes(AttributeDto dto);
        Task<AttributeDto> GetAttributeById(Guid id);
    }

    public class AttributesServices : IAttributeService
    {
        private IAttributeReposiotories _repository;
        private readonly IMapper _mapper;
        private readonly IExternalEventProducer _externalEventProducer;
        public AttributesServices(IAttributeReposiotories reposiotory, IExternalEventProducer externalEventProducer, IMapper mapper)
        {
            _repository = reposiotory;
            _mapper = mapper;
            _externalEventProducer = externalEventProducer;
        }
        public async Task<AttributeDto> AddAttribute(AttributeDto dto)
        {
            if (dto != null)
            {
                var dtoToEntity = _mapper.Map<AttributesEntity>(dto);
                var entity = await _repository.Add(dtoToEntity);
                var result = await _repository.SaveChangeAsync();
                if (result > 0)
                {
                    var externalEvent = new EventEnvelope<AttributeCreated>(
                        AttributeCreated.Create(
                        id: entity.Id,
                        unit: entity.Unit,
                        type: entity.Type,
                        status: entity.Status
                        ));
                    await _externalEventProducer.Publish(externalEvent, new CancellationToken());
                    return _mapper.Map<AttributeDto>(entity);
                }
            }
            return new AttributeDto();
        }

        public async Task<IEnumerable<AttributeDto>> GetAllAttributes()
        {
            return _mapper.Map<IEnumerable<AttributeDto>>(await _repository.GetAll());
        }

        public async Task<AttributeDto> UpdateAttributes(AttributeDto dto)
        {
            if (dto != null)
            {

                var dtoToEntity = _mapper.Map<AttributesEntity>(dto);
                var entity = await _repository.Update(dtoToEntity);
                var result = await _repository.SaveChangeAsync();
                if(result > 0)
                {
                    var externalEvent = new EventEnvelope<AttributeUpdated>(
                        AttributeUpdated.Updated(
                            id: entity.Id,
                            unit: entity.Unit,
                            type: entity.Type,
                            status: entity.Status
                            )
                        );
                    await _externalEventProducer.Publish(externalEvent, new CancellationToken());
                    return _mapper.Map<AttributeDto>(entity);
                }
            }
            return new AttributeDto();
        }

        public async Task<AttributeDto> GetAttributeById(Guid id)
        {
            var result = _mapper.Map<AttributeDto>(await _repository.GetById(id));
            return result;
        }
    }
}