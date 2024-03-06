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
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAll();
        Task<ProductDto> Add(ProductInputDto dto);
        Task<ProductDto> GetById(Guid id);
        Task<ProductDto?> Updates(ProductUpdateDto dto);
        Task<ProductDto?> ChangeCategory(ProductCategoryChangedDto dto);
        Task<ProductDto?> ChangeAttribute(ProductAttributeChangedDto dto);
        Task<ProductDto?> ChangePriceVolume(ProductPriceVolumeChangeDto dto);
        Task<ProductDto?> ChangeStockSold(ProductSoldStockChangeDto dto);
        Task<ProductDto?> ChangeStatus(ProductStatusChangeDto dto);
    }
    public class ProductService: IProductService
    {
        private IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IExternalEventProducer _externalEventProducer;
        public ProductService(IProductRepository repository, IMapper mapper, IExternalEventProducer externalEventProducer)
        {
            _repository = repository;
            _mapper = mapper;
            _externalEventProducer = externalEventProducer;
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<ProductDto>>(await _repository.GetAll());
        }

        public async Task<ProductDto> Add(ProductInputDto dto)
        {
            if (dto != null)
            {
                var dtoToEntity = _mapper.Map<ProductEntity>(dto);
                var entity = await _repository.Add(dtoToEntity);
                var result = await _repository.SaveChangeAsync();
                if (result > 0)
                {
                    var externalEvent = new EventEnvelope<ProductCreated>(
                        ProductCreated.Create(
                        id: entity.Id,
                        name: entity.Name,
                        description: entity.Sku,
                        sku: entity.Sku,
                        categoryId: entity.CategoryId,
                        attributeId: entity.AttributeId,
                        status: entity.Status
                        ));
                    await _externalEventProducer.Publish(externalEvent, new CancellationToken());
                    return _mapper.Map<ProductDto>(entity);
                }
            }
            return new ProductDto();
        }

        public async Task<ProductDto> Updates(ProductUpdateDto dto)
        {
            try
            {
                if (dto.Id != new Guid())
                {
                    var exist = await _repository.GetById((Guid)dto.Id);
                    if (exist != null)
                    {
                        var dtoToEntity = _mapper.Map<ProductUpdateDto, ProductEntity>(dto, exist);
                        dtoToEntity.ModifiedDate = DateTime.Now;
                        var entity = await _repository.Update(dtoToEntity);
                        var result = await _repository.SaveChangeAsync();
                        if (result > 0)
                        {
                            var externalEvent = new EventEnvelope<ProductUpdated>(
                                ProductUpdated.Create(
                                    id: entity.Id,
                                    name: entity.Name,
                                    description: entity.Description,
                                    sku: entity.Sku
                                    )
                                );
                            await _externalEventProducer.Publish(externalEvent, new CancellationToken());
                            return _mapper.Map<ProductDto>(entity);
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            return new ProductDto();
        }

        public Task<ProductDto> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDto> ChangeStatus(ProductStatusChangeDto dto)
        {
            try
            {
                if (dto.Id != new Guid())
                {
                    var exist = await _repository.GetById((Guid)dto.Id);
                    if (exist != null)
                    {
                        var dtoToEntity = _mapper.Map<ProductStatusChangeDto, ProductEntity>(dto, exist);
                        dtoToEntity.ModifiedDate = DateTime.Now;
                        var entity = await _repository.Update(dtoToEntity);
                        var result = await _repository.SaveChangeAsync();
                        if (result > 0)
                        {
                            var externalEvent = new EventEnvelope<ProductStatusChanged>(
                                ProductStatusChanged.Created(
                                    id: entity.Id,
                                    status: entity.Status
                                    )
                                );
                            await _externalEventProducer.Publish(externalEvent, new CancellationToken());
                            return _mapper.Map<ProductDto>(entity);
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            return new ProductDto();
        }

        public async Task<ProductDto> ChangeCategory(ProductCategoryChangedDto dto)
        {
            try
            {
                if (dto.Id != new Guid())
                {
                    var exist = await _repository.GetById((Guid)dto.Id);
                    if (exist != null)
                    {
                        var dtoToEntity = _mapper.Map<ProductCategoryChangedDto, ProductEntity>(dto, exist);
                        dtoToEntity.ModifiedDate = DateTime.Now;
                        var entity = await _repository.Update(dtoToEntity);
                        var result = await _repository.SaveChangeAsync();
                        if (result > 0)
                        {
                            var externalEvent = new EventEnvelope<ProductCategoryChanged>(
                                ProductCategoryChanged.Created(
                                    id: entity.Id,
                                    categoryId: entity.CategoryId
                                    )
                                );
                            await _externalEventProducer.Publish(externalEvent, new CancellationToken());
                            return _mapper.Map<ProductDto>(entity);
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            return new ProductDto();
        }

        public async Task<ProductDto> ChangeAttribute(ProductAttributeChangedDto dto)
        {
            try
            {
                if (dto.Id != new Guid())
                {
                    var exist = await _repository.GetById((Guid)dto.Id);
                    if (exist != null)
                    {
                        var dtoToEntity = _mapper.Map<ProductAttributeChangedDto, ProductEntity>(dto, exist);
                        dtoToEntity.ModifiedDate = DateTime.Now;
                        var entity = await _repository.Update(dtoToEntity);
                        var result = await _repository.SaveChangeAsync();
                        if (result > 0)
                        {
                            var externalEvent = new EventEnvelope<ProductAttributeChanged>(
                                ProductAttributeChanged.Created(
                                    id: entity.Id,
                                    attributeId: entity.AttributeId
                                    )
                                );
                            await _externalEventProducer.Publish(externalEvent, new CancellationToken());
                            return _mapper.Map<ProductDto>(entity);
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            return new ProductDto();
        }

        public async Task<ProductDto> ChangePriceVolume(ProductPriceVolumeChangeDto dto)
        {
            try
            {
                if (dto.Id != new Guid())
                {
                    var exist = await _repository.GetById((Guid)dto.Id);
                    if (exist != null)
                    {
                        var dtoToEntity = _mapper.Map<ProductPriceVolumeChangeDto, ProductEntity>(dto, exist);
                        dtoToEntity.ModifiedDate = DateTime.Now;
                        var entity = await _repository.Update(dtoToEntity);
                        var result = await _repository.SaveChangeAsync();
                        if (result > 0)
                        {
                            var externalEvent = new EventEnvelope<ProductPriceVolumeChanged>(
                                ProductPriceVolumeChanged.Created(
                                    id: entity.Id,
                                    price: entity.Price,
                                    volume: entity.Volume
                                    )
                                );
                            await _externalEventProducer.Publish(externalEvent, new CancellationToken());
                            return _mapper.Map<ProductDto>(entity);
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            return new ProductDto();
        }

        public async Task<ProductDto> ChangeStockSold(ProductSoldStockChangeDto dto)
        {
            try
            {
                if (dto.Id != new Guid())
                {
                    var exist = await _repository.GetById((Guid)dto.Id);
                    if (exist != null)
                    {
                        var dtoToEntity = _mapper.Map<ProductSoldStockChangeDto, ProductEntity>(dto, exist);
                        dtoToEntity.ModifiedDate = DateTime.Now;
                        var entity = await _repository.Update(dtoToEntity);
                        var result = await _repository.SaveChangeAsync();
                        if (result > 0)
                        {
                            var externalEvent = new EventEnvelope<ProductSoldStockChanged>(
                                ProductSoldStockChanged.Created(
                                    id: entity.Id,
                                    sold: entity.Sold,
                                    stock: entity.Stock
                                    )
                                );
                            await _externalEventProducer.Publish(externalEvent, new CancellationToken());
                            return _mapper.Map<ProductDto>(entity);
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            return new ProductDto();
        }
    }
}
