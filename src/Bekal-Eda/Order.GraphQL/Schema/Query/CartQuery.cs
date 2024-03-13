using Order.Domain.Dtos;
using Order.Domain.Services;

namespace Order.GraphQL.Schema.Query
{

    [ExtendObjectType(typeof(Query))]
    public class CartQuery
    {
        private readonly ICartService _service;
        public CartQuery(ICartService service)
        {
            _service = service;
        }
        public async Task<IEnumerable<CartDto>> GetCartAll()
        {
            IEnumerable<CartDto> result = await _service.All();
            return result;
        }
        public async Task<CartDto> GetCartById(Guid id)
        {
            return await _service.GetCartById(id);
        }
    }
}
