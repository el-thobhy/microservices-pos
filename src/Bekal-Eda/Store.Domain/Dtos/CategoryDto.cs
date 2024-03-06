using Framework.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Dtos
{
    public class CategoryDto
    {
        public Guid? Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Descriprion { get; set; } = default!;
    }
    public class CategoryStatusDto
    {
        public Guid Id { get; set; } = default!;
        public RecordStatusEnum Status { get; set; } = RecordStatusEnum.Inactive;
    }
}
