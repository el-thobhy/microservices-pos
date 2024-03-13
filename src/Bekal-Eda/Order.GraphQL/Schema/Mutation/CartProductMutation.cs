using HotChocolate.Authorization;
using Order.Domain.Dtos;
using Order.Domain.Services;

namespace Order.GraphQL.Schema.Mutation
{
    [ExtendObjectType(typeof(Mutation))]
    public class CartProductMutation
    {
        private readonly ICartProductService _service;

        public CartProductMutation(ICartProductService service)
        {
            _service = service;
        }
        [Authorize(Roles = new[] { "administrator", "customer" })]
        public async Task<CartProductDto> addCartProduct(CartProductInputDto input)
        {
            var result = await _service.AddCartProduct(input);

            return result;
        }
    }
}
