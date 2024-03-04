using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Entity
{
    public class BaseEntity
    {
        public Nullable<Guid> ModifiedBy { get; set; } = default!;
        public Nullable<DateTime> ModifiedDate { get; set; } = DateTime.Now;
    }
}
