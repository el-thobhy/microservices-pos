using Product.Domain.Dto;
using Product.Domain.Schema;
using Product.Domain.Services;

namespace CreateProductServices.Schema.Mutation
{
    [ExtendObjectType(typeof(Query))]
    public class EmptyQuery
    {
        private readonly IProductQueryService _service;
        public EmptyQuery(IProductQueryService service)
        {
            _service = service;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductTesCreateAsync()
        {
            return await _service.GetAllProduct();
        }

        public async Task<ProductDto> GetProductTesCreateById(Guid id)
        {
            return await _service.GetProductById(id);
        }
    }
}
