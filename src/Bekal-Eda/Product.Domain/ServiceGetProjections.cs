using Framework.Core.Projection;
using Microsoft.Extensions.DependencyInjection;
using Product.Domain.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain
{
    public static class ServiceGetProjections
    {
        public static IServiceCollection AddProduct(this IServiceCollection services)
          =>
            services.Projection(
                builder => builder.AddOn<ProductCreated>(ProductProjections.Handle)
            );
    }
}
