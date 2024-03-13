using AutoMapper;
using Framework.Core.Event.External;
using Order.Domain.Dtos;
using Order.Domain.Entities;
using Order.Domain.Repositories;

namespace Order.Domain.Services
{
    public interface ICartProductService
    {
        Task<CartProductDto> AddCartProduct(CartProductInputDto dto);
        Task<bool> UpdateCartProduct(CartProductDto dto);
        Task<IEnumerable<CartProductDto>> GetAllCartProduct();
        Task<CartProductDto> GetCartProductById(Guid id);
    }
    public class CartProductService : ICartProductService
    {
        private readonly ICartProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IExternalEventProducer _externalEventProducer;
        public CartProductService(ICartProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CartProductDto> AddCartProduct(CartProductInputDto dto)
        {
            var product = await _repository.GetProductById(dto.ProductId);
            if (product == null)
            {
                return null;
            }
            var existCp = await _repository.GetCartById(dto.CartId, dto.ProductId);
            if(existCp != null)
            {
                if(product.Stock >= dto.Quantity + existCp.Quantity)
                {
                    existCp.Quantity = dto.Quantity + existCp.Quantity;
                }
                else
                {
                    existCp.Quantity = product.Stock;
                }
                existCp.Name = product.Name;
                var entity = await _repository.Update(existCp);
                var result = await _repository.SaveChangesAsync();
                if(result > 0)
                {
                    return _mapper.Map<CartProductDto>(entity);
                }
            }
            else
            {
                var entity = await _repository.Add(_mapper.Map<CartProductEntity>(dto));
                entity.Name = product.Name;
                entity.Sku = product.Sku;
                entity.Price = product.Price;
                var result = await _repository.SaveChangesAsync();
                if (result > 0)
                    return _mapper.Map<CartProductDto>(entity);
            }
            return new CartProductDto();
        }

        public async Task<IEnumerable<CartProductDto>> GetAllCartProduct()
        {
            return _mapper.Map<IEnumerable<CartProductDto>>(await _repository.GetAll());
        }

        public async Task<CartProductDto> GetCartProductById(Guid id)
        {
            if (id != Guid.Empty)
            {
                var cartProduct = await _repository.GetById(id);
                return _mapper.Map<CartProductDto>(cartProduct);
            }
            return null;
        }

        public async Task<bool> UpdateCartProduct(CartProductDto dto)
        {
            var entity = await _repository.Update(_mapper.Map<CartProductEntity>(dto));
            var result = await _repository.SaveChangesAsync();
            if (result > 0) return true;
            return false;
        }
    }
}
