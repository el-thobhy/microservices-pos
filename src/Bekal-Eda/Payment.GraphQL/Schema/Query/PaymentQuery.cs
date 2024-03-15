using Payment.Domain.Dtos;
using Payment.Domain.Services;

namespace Payment.GraphQL.Schema.Query
{
    [ExtendObjectType(typeof(Query))]
    public class PaymentQuery
    {
        private readonly IPaymentService _service;
        public PaymentQuery(IPaymentService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<PaymentDto>> GetAllAsync()
        {
            return await _service.All();
        }
    }
}
