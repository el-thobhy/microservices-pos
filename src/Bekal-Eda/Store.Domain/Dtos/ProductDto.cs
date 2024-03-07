using Framework.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid AttributeId { get; set; }
        public string Sku { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public decimal Volume { get; set; } = default!;
        public int Sold { get; set; } = default!;
        public int Stock { get; set; } = default!;
        public CategoryDto? Category { get; set; }
        public AttributeDto? Attribute { get; set; }
        public RecordStatusEnum Status { get; set; } = RecordStatusEnum.Inactive;
    }
    public class ProductCreateDto
    {
        public Guid CategoryId { get; set; }
        public Guid AttributeId { get; set; }
        public string Sku { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
    public class ProductUpdateDto
    {
        public Guid Id { get; set; }
        public string Sku { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
    public class ProductCategoryChangedDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
    }
    public class ProductAttributeChangedDto
    {
        public Guid Id { get; set; }
        public Guid AttributeId { get; set; }
    }
    public class ProductPriceVolumeChangeDto
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; } = default!;
        public decimal Volume { get; set; } = default!;
    }
    public class ProductSoldStockChangeDto
    {
        public Guid Id { get; set; }
        public int Sold { get; set; } = default!;
        public int Stock { get; set; } = default!;
    }
    public class ProductStatusChangeDto
    {
        public Guid Id { get; set; }
        public RecordStatusEnum Status { get; set; } = RecordStatusEnum.Inactive;
    }
}
