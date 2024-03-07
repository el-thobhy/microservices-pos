using Store.Domain.Dtos;
using Store.Domain.Services;

namespace Store.GraphQL.Schema.Query
{
    [ExtendObjectType(typeof(Query))]
    public class ProductQuery
    {
        private readonly IProductService _service;
        public ProductQuery(IProductService service)
        {
            _service = service;
        }
        //[UsePaging]
        public async Task<IEnumerable<ProductDto>> GetAllProductAsync()
        {
            return await _service.GetAllProduct();
        }

        public async Task<ProductDto> GetProductById(Guid id)
        {
            return await _service.GetProductById(id);
        }

    }
}
