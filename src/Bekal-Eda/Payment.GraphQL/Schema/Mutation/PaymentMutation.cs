using Payment.Domain.Dtos;
using Payment.Domain.Services;

namespace Payment.GraphQL.Schema.Mutation
{
    [ExtendObjectType(typeof(Mutation))]
    public class PaymentMutation
    {
        private readonly IPaymentService _service;
        public PaymentMutation(IPaymentService service)
        {
            _service = service;
        }

        public async Task<PaymentDto> Payment(Guid cartId, decimal amount)
        {
            return await _service.Paying(cartId, amount);
        }
    }
}
