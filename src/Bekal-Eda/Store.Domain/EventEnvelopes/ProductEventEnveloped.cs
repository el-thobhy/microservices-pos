using Framework.Core.Enums;

namespace Store.Domain.EventEnvelopes
{
    public record ProductCreated(
        Guid Id,
         Guid CategoryId,
         Guid AttributeId,
         string Sku,
         string Name,
         string Description,
        RecordStatusEnum Status
        )
    {
        public static ProductCreated Create(
            Guid id,
            Guid categoryId,
         Guid attributeId,
         string sku,
         string name,
         string description,
        RecordStatusEnum status) => new(id, categoryId, attributeId, sku, name, description, status);
    }

    public record ProductUpdated(
         Guid Id,
         string Sku,
         string Name,
         string Description)
    {
        public static ProductUpdated Create(Guid id,
         string sku,
         string name,
         string description) => new(id, sku, name, description);
    }

    public record ProductCategoryChanged(
        Guid Id, 
        Guid CategoryId)
    {
        public static ProductCategoryChanged Created(Guid id, Guid categoryId)
            => new(id, categoryId);
    }

    public record ProductAttributeChanged(
        Guid Id, 
        Guid AttributeId)
    {
        public static ProductAttributeChanged Created(Guid id, Guid attributeId)
            => new(id, attributeId);
    }
    public record ProductPriceVolumeChanged(
        Guid Id, 
        decimal Price, 
        decimal Volume)
    {
        public static ProductPriceVolumeChanged Created(Guid id, decimal price, decimal volume)
            => new(id, price, volume);
    }

    public record ProductSoldStockChanged(
        Guid Id, 
        int Sold, 
        int Stock)
    {
        public static ProductSoldStockChanged Created(Guid id, int sold, int stock)
            => new(id, sold, stock);
    }
    public record ProductStatusChanged(
        Guid Id, 
        RecordStatusEnum Status)
    {
        public static ProductStatusChanged Created(Guid id, RecordStatusEnum status)
            => new(id, status);
    }
}
