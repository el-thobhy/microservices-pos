using Framework.Core.Projection;
using Microsoft.Extensions.DependencyInjection;
using Payment.Domain.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain
{
    public static class PaymentService
    {
        public static IServiceCollection AddPayment(this IServiceCollection services) =>
            services
                .Projection(builder => builder
                    .AddOn<UserCreated>(UserProjection.Handle)
                    .AddOn<ProductCreated>(ProductProjection.Handle)
                    .AddOn<ProductUpdated>(ProductProjection.HandleUpdated)
                    .AddOn<ProductPriceVolumeChanged>(ProductProjection.HandlePriceVolumeChanged)
                    .AddOn<ProductSoldStockChanged>(ProductProjection.HandleSoldStockChanged)
                    .AddOn<ProductStatusChanged>(ProductProjection.HandleChangeStatus)
            );
        public static IServiceCollection AddCart(this IServiceCollection services) =>
           services
               .Projection(builder => builder
                    .AddOn<CartCreated>(CartProjection.Handle)
                    .AddOn<CartStatusChanged>(CartProjection.Handle)
           );
    }
}
