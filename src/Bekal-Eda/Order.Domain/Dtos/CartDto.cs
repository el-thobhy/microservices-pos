using Framework.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Dtos
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public CartStatusEnum Status { get; set; } = CartStatusEnum.Pending;
        public UserDto Customer { get; set; }
    }
}
