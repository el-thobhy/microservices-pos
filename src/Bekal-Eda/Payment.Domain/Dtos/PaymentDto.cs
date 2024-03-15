using Framework.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain.Dtos
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal? Total { get; set; }
        public decimal? Pay { get; set; }
        public CartStatusEnum Status { get; set; }
        public DateTime Modified { get; set; }
        public UserDto Customer { get; set; }
    }
}
