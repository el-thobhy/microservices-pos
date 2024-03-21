using Product.Domain.Dto;
using Product.Domain.Services;

namespace Product.Domain.Schema
{
    [ExtendObjectType(typeof(Mutation))]
    public class ProductMutation
    {
        private readonly IProductCommandService _service;
        public ProductMutation(IProductCommandService service)
        {
            _service = service;
        }
        public async Task<ProductDto> AddProductTesAsync(ProductDto dto)
        {
            var result = await _service.AddProduct(dto);
            return result;

        }
        public async Task<ProductDto> UpdateTesProduct(ProductDto dto)
        {
            var result = await _service.UpdateProduct(dto);
            return result;
        }
    }
}
