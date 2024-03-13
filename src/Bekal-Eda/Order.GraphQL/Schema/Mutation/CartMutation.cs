using Framework.Core.Enums;
using HotChocolate.Authorization;
using Order.Domain.Dtos;
using Order.Domain.Services;

namespace Order.GraphQL.Schema.Mutation
{
    [ExtendObjectType(typeof(Mutation))]
    public class CartMutation
    {
        private readonly ICartService _service;

        public CartMutation(ICartService service)
        {
            _service = service;
        }
        [Authorize(Roles = new[] { "administrator", "customer" })]
        public async Task<CartDto> addCart(CartInputDto input)
        {
            CartDto dto = new CartDto();
            dto.CustomerId = input.CustomerId;
            var result = await _service.AddCart(dto);

            return result;
        }

        [Authorize(Roles = new[] { "administrator", "customer" })]
        public async Task<CartDto> confirmCartStatus(Guid id)
        {
            var result = await _service.UpdateCartStatus(CartStatusEnum.Confirmed, id);
            if (!result)
            {
                throw new GraphQLException(new Error("Cart not found", "404"));
            }

            return await _service.GetCartById(id);
        }
    }
}
