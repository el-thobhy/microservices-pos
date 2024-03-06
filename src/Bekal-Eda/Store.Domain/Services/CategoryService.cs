using AutoMapper;
using Framework.Core.Event;
using Framework.Core.Event.External;
using Store.Domain.Dtos;
using Store.Domain.Entities;
using Store.Domain.EventEnvelopes;
using Store.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAlls();
        Task<CategoryDto> Add(CategoryDto dto);
        Task<bool> Updates(CategoryDto dto);
        Task<CategoryDto> GetById(Guid id);
        Task<bool> ChangeStatus(CategoryStatusDto dto);
    }
    public class CategoryService: ICategoryService
    {
        private ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly IExternalEventProducer _externalEventProducer;
        public CategoryService(ICategoryRepository reposiotory, IExternalEventProducer externalEventProducer, IMapper mapper)
        {
            _repository = reposiotory;
            _mapper = mapper;
            _externalEventProducer = externalEventProducer;
        }

        public async Task<CategoryDto> Add(CategoryDto dto)
        {
            if (dto != null)
            {
                var dtoToEntity = _mapper.Map<CategoryEntity>(dto);
                var entity = await _repository.Add(dtoToEntity);
                var result = await _repository.SaveChangeAsync();
                if (result > 0)
                {
                    var externalEvent = new EventEnvelope<CategoryCreated>(
                        CategoryCreated.Create(
                        id: entity.Id,
                        name: entity.Name,
                        description: entity.Descriprion,
                        status: entity.Status
                        ));
                    await _externalEventProducer.Publish(externalEvent, new CancellationToken());
                    return _mapper.Map<CategoryDto>(entity);
                }
            }
            return new CategoryDto();
        }

        public async Task<bool> ChangeStatus(CategoryStatusDto dto)
        {
            try
            {
                if (dto.Id != new Guid())
                {
                    var exist = await _repository.GetById((Guid)dto.Id);
                    if (exist != null)
                    {
                        var dtoToEntity = _mapper.Map<CategoryStatusDto, CategoryEntity>(dto, exist);
                        dtoToEntity.ModifiedDate = DateTime.Now;
                        var entity = await _repository.Update(dtoToEntity);
                        var result = await _repository.SaveChangeAsync();
                        if (result > 0)
                        {
                            var externalEvent = new EventEnvelope<CategoryStatusChange>(
                                CategoryStatusChange.Create(
                                    id: entity.Id,
                                    status: entity.Status
                                    )
                                );
                            await _externalEventProducer.Publish(externalEvent, new CancellationToken());
                            return true;
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            return false;
        }

        public async Task<IEnumerable<CategoryDto>> GetAlls()
        {
            return _mapper.Map<IEnumerable<CategoryDto>>(await _repository.GetAll());
        }

        public async Task<CategoryDto> GetById(Guid id)
        {
            var result = _mapper.Map<CategoryDto>(await _repository.GetById(id));
            return result;
        }

        public async Task<bool> Updates(CategoryDto dto)
        {
            try
            {
                if (dto.Id != new Guid())
                {
                    var exist = await _repository.GetById((Guid)dto.Id);
                    if (exist != null)
                    {
                        var dtoToEntity = _mapper.Map<CategoryDto, CategoryEntity>(dto, exist);
                        dtoToEntity.ModifiedDate = DateTime.Now;
                        var entity = await _repository.Update(dtoToEntity);
                        var result = await _repository.SaveChangeAsync();
                        if (result > 0)
                        {
                            var externalEvent = new EventEnvelope<CategoryUpdated>(
                                CategoryUpdated.Create(
                                    id: entity.Id,
                                    name: entity.Name,
                                    description: entity.Descriprion
                                    )
                                );
                            await _externalEventProducer.Publish(externalEvent, new CancellationToken());
                            return true;
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            return false;
        }
    }
}
