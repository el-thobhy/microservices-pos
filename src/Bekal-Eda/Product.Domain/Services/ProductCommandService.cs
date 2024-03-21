using AutoMapper;
using Framework.Core.Event;
using Framework.Core.Event.External;
using Product.Domain.Dto;
using Product.Domain.Entity;
using Product.Domain.Event;
using Product.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Services
{
    public interface IProductCommandService
    {
        Task<ProductDto> AddProduct(ProductDto dto);
        Task<ProductDto> UpdateProduct(ProductDto dto);
    }
    public class ProductCommandService : IProductCommandService
    {
        private IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IExternalEventProducer _producer;
        public ProductCommandService(IProductRepository repository, IMapper mapper,IExternalEventProducer producer)
        {
            _mapper = mapper;
            _repository = repository;
            _producer = producer;
        }
        public async Task<ProductDto> AddProduct(ProductDto dto)
        {
            var dtoToEntity = _mapper.Map<ProductEntity>(dto);
            var entity = await _repository.Add(dtoToEntity);

            var result = await _repository.SaveChangeAsync();
            if (result > 0)
            {
                var producerEvent = new EventEnvelope<ProductCreated>(
                    ProductCreated.Create(
                        id: entity.Id,
                        name: entity.Name,
                        description: entity.Description,
                        price: entity.Price
                    ));
                await _producer.Publish(producerEvent, new CancellationToken());
                return _mapper.Map<ProductDto>(entity);
            }
            return new ProductDto();
        }

        public async Task<ProductDto> UpdateProduct(ProductDto dto)
        {
            var exist = await _repository.GetById((Guid)dto.Id);
            if (exist != null)
            {
                var dtoToEntity = _mapper.Map<ProductDto, ProductEntity>(dto, exist);
                var entity = await _repository.Update(dtoToEntity);
                await _repository.SaveChangeAsync();
                return _mapper.Map<ProductDto>(entity);
            }
            return new ProductDto();
        }
    }
}
