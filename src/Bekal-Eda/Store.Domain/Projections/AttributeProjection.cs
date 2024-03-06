using Framework.Core.Enums;
using Framework.Core.Event;
using Store.Domain.Entities;

namespace Store.Domain.Projections
{
    public record AttributeCreated
    (
        Guid? Id,
        AttributeTypeEnum Type,
        string Unit,
        RecordStatusEnum Status
    );
    public record AttributeUpdated
    (
        Guid? Id,
        AttributeTypeEnum Type,
        string Unit,
        RecordStatusEnum Status
    );

    public class AttributeProjection
    {
        public static bool Handle(EventEnvelope<AttributeCreated> eventEnvelope)
        {
            var (id, type, unit, status) = eventEnvelope.Data;
            using (var context = new StoreDbContext(StoreDbContext.OnConfigure()))
            {
                AttributesEntity entity = new AttributesEntity()
                {
                    Id = (Guid)id,
                    Type = type,
                    Unit = unit,
                    Status = status
                };
                context.Add(entity);
                context.SaveChanges();
            }
            return true;
        }
        public static bool HandleUdpdate(EventEnvelope<AttributeUpdated> eventEnvelope)
        {
            var (id, type, unit, status) = eventEnvelope.Data;
            using (var context = new StoreDbContext(StoreDbContext.OnConfigure()))
            {
                
                AttributesEntity? entity = context.Attributes.Where(a => a.Id == id).FirstOrDefault();
                if (entity != null)
                {
                    entity.Id = (Guid)id;
                    entity.Type = type;
                    entity.Unit = unit;
                    entity.Status = status;
                }
                context.SaveChanges();
            }
            return true;
        }
    }
}
