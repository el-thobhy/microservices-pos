using Product.Domain.Dto;
using Product.Domain.Services;

namespace Product.Domain.Schema
{
    [ExtendObjectType(typeof(Query))]
    public class ProductQuery
    {
        private readonly IProductQueryService _service;
        public ProductQuery(IProductQueryService service)
        {
            _service = service;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductTesAsync()
        {
            return await _service.GetAllProduct();
        }

        public async Task<ProductDto> GetProductTesById(Guid id)
        {
            return await _service.GetProductById(id);
        }

    }
}
