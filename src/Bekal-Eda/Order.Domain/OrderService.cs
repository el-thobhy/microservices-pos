using Framework.Core.Projection;
using Microsoft.Extensions.DependencyInjection;
using Order.Domain.Projection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain
{
    public static class OrderService
    {
        public static IServiceCollection AddUser(this IServiceCollection services)
        {
            return services.Projection(
                builder => builder.AddOn<UserCreated>(UserProjection.Handle)
                );
        }
        public static IServiceCollection AddProduct(this IServiceCollection services)
        {
            return services.Projection(
                builder => builder
                .AddOn<ProductCreated>(ProductProjection.HandleCreated)
                .AddOn<ProductUpdated>(ProductProjection.HandleUpdated)
                .AddOn<ProductPriceVolumeChanged>(ProductProjection.HandlePriceVolumeChanged)
                .AddOn<ProductSoldStockChanged>(ProductProjection.HandleSoldStockChanged)
                .AddOn<ProductStatusChanged>(ProductProjection.HandleChangeStatus)
                );
        }
    }
}
