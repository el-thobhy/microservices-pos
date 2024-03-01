using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookUp.Domain.Dtos
{
    public class AttributesDto
    {
        public Guid Id { get; set; }
        public AttributeTypeEnum Type { get; set; } = AttributeTypeEnum.Text;
        public string Unit { get; set; } = default!;
        public LookUpStatusEnum Status { get; set; } = LookUpStatusEnum.Inactive;
    }
}
