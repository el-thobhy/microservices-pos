using HotChocolate.Authorization;
using Order.Domain.Dtos;
using Order.Domain.Services;

namespace Order.GraphQL.Schema.Query
{
    [ExtendObjectType(typeof(Query))]
    public class CartProductQuery
    {
        private readonly ICartProductService _service;

        public CartProductQuery(ICartProductService service)
        {
            _service = service;
        }
        [Authorize(Roles = new[] { "administrator", "customer" })]
        public async Task<IEnumerable<CartProductDto>> getAllCartProduct()
        {
            IEnumerable<CartProductDto> result = await _service.GetAllCartProduct();
            return result;
        }

        [Authorize(Roles = new[] { "administrator", "customer" })]
        public async Task<CartProductDto> getCartProductById(Guid id)
        {
            CartProductDto result = await _service.GetCartProductById(id);
            return result;
        }

    }
}
