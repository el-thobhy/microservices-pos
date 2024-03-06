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
        string Unit
    );
    public record AttributeStatusChanged(
       Guid? Id,
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
            try
            {
                var (id, type, unit) = eventEnvelope.Data;
                using (var context = new StoreDbContext(StoreDbContext.OnConfigure()))
                {
                
                    AttributesEntity? entity = context.Set<AttributesEntity>().Find(id);
                    if (entity != null)
                    {
                        entity.Type = type;
                        entity.Unit = unit;
                    }
                    context.Update(entity);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }
            return true;
        }
        public static bool HandleChangeStatus(EventEnvelope<AttributeStatusChanged> eventEnvelope)
        {
            try
            {
                var (id, status) = eventEnvelope.Data;
                using (var context = new StoreDbContext(StoreDbContext.OnConfigure()))
                {

                    AttributesEntity? entity = context.Set<AttributesEntity>().Find(id);
                    if (entity != null)
                    {
                        entity.Status = status;
                    }
                    context.Update(entity);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }
            return true;
        }
    }
}
