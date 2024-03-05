using Framework.Core.Projection;
using Microsoft.Extensions.DependencyInjection;
using Store.Domain.Projections;

namespace Store.Domain.Services
{
    public static class StoreServices
    {
        public static IServiceCollection AddStore(this IServiceCollection services)
          =>
            services.Projection(
                builder => builder.AddOn<AttributeCreated>(AttributeProjection.Handle)
            );
    }
}
