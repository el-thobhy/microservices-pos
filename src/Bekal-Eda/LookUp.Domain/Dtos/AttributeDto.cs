using Framework.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookUp.Domain.Dtos
{
    public class AttributeDto
    {
        public Guid? Id { get; set; }
        public AttributeTypeEnum Type { get; set; } = AttributeTypeEnum.Text;
        public string Unit { get; set; } = default!;
        public RecordStatusEnum Status { get; set; } = RecordStatusEnum.Inactive;
    }
    public class AttributeExceptStatusDto
    {
        public Guid? Id { get; set; }
        public AttributeTypeEnum Type { get; set; } = AttributeTypeEnum.Text;
        public string Unit { get; set; } = default!;
    }
    public class AttributeStatusDto
    {
        public Guid? Id { get; set; }
        public RecordStatusEnum Status { get; set; } = RecordStatusEnum.Inactive;
    }
}
