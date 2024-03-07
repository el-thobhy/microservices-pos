using Framework.Core.Enums;
using Framework.Core.Event;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Projection
{
    public record ProductCreated(
         Guid Id,
         string Sku,
         string Name,
         decimal Price,
         decimal Volume,
         int Sold,
         int Stock,
         RecordStatusEnum Status
        );
    public record ProductStatusChanged(
        Guid Id,
        RecordStatusEnum Status);
    public record ProductSoldStockChanged(
        Guid Id,
        int Sold,
        int Stock);

    public record ProductPriceVolumeChanged(
        Guid Id,
        decimal Price,
        decimal Volume);
    public record ProductUpdated(
        Guid Id,
        string Sku,
        string Name);

    public class ProductProjection
    {
        public static bool HandleCreated(EventEnvelope<ProductCreated> eventEnvelope)
        {
            var (id, sku, name, price, volume, sold, stock, status) = eventEnvelope.Data;
            using (var context = new OrderDbContext(OrderDbContext.OnConfigure()))
            {
                ProductEntity entity = new ProductEntity()
                {
                    Id = (Guid)id,
                    Sku = sku,
                    Name = name,
                    Price = price,
                    Volume = volume,
                    Sold = sold,
                    Stock = stock,
                    Status = status,
                };
                context.Add(entity);
                context.SaveChanges();
            }
            return true;
        }
        public static bool HandleUpdated(EventEnvelope<ProductUpdated> eventEnvelope)
        {
            try
            {
                var (id, sku, name) = eventEnvelope.Data;
                using (var context = new OrderDbContext(OrderDbContext.OnConfigure()))
                {
                    ProductEntity? entity = context.Set<ProductEntity>().Find(id);
                    if (entity != null)
                    {
                        entity.Id = (Guid)id;
                        entity.Sku = sku;
                        entity.Name = name;

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
        public static bool HandlePriceVolumeChanged(EventEnvelope<ProductPriceVolumeChanged> eventEnvelope)
        {
            try
            {
                var (id, price, volume) = eventEnvelope.Data;
                using (var context = new OrderDbContext(OrderDbContext.OnConfigure()))
                {
                    ProductEntity? entity = context.Set<ProductEntity>().Find(id);
                    if (entity != null)
                    {
                        entity.Price = price;
                        entity.Volume = volume;
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
        public static bool HandleSoldStockChanged(EventEnvelope<ProductSoldStockChanged> eventEnvelope)
        {
            try
            {
                var (id, sold, stock) = eventEnvelope.Data;
                using (var context = new OrderDbContext(OrderDbContext.OnConfigure()))
                {
                    ProductEntity? entity = context.Set<ProductEntity>().Find(id);
                    if (entity != null)
                    {
                        entity.Sold = sold;
                        entity.Stock = stock;
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
        public static bool HandleChangeStatus(EventEnvelope<ProductStatusChanged> eventEnvelope)
        {
            try
            {
                var (id, status) = eventEnvelope.Data;
                using (var context = new OrderDbContext(OrderDbContext.OnConfigure()))
                {

                    ProductEntity? entity = context.Set<ProductEntity>().Find(id);
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
