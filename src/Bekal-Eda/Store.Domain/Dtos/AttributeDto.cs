using Framework.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Dtos
{
    public class AttributeDto
    {
        public Guid? Id { get; set; }
        public AttributeTypeEnum Type { get; set; }
        public string Unit { get; set; } = default!;
        public RecordStatusEnum Status { get; set; }
    }
}
