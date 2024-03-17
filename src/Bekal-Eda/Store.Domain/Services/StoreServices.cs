using FluentValidation;
using Framework.Core.Projection;
using Microsoft.Extensions.DependencyInjection;
using Store.Domain.Dtos;
using Store.Domain.Projections;
using Store.Domain.Validations;

namespace Store.Domain.Services
{
    public static class StoreServices
    {
        public static IServiceCollection AddStore(this IServiceCollection services)
          =>
            services.Projection(
                builder => builder.AddOn<AttributeCreated>(AttributeProjection.Handle)
            );
        public static IServiceCollection UpdateStoreAttribute(this IServiceCollection services)
          =>
            services.Projection(
                builder => builder.AddOn<AttributeUpdated>(AttributeProjection.HandleUdpdate)
            );
        public static IServiceCollection ChangeStatusAttribute(this IServiceCollection services)
         =>
           services.Projection(
               builder => builder.AddOn<AttributeStatusChanged>(AttributeProjection.HandleChangeStatus)
           );

        public static IServiceCollection AddValidator(this IServiceCollection services)
        {
            services
                .AddScoped<IValidator<ProductCreateDto>, ProductCreateValidator>()
                .AddScoped<IValidator<CategoryStatusDto>, CategoryChangeStatusValidator>()
                .AddScoped<IValidator<CategoryInputDto>, CategoryCreateValidator>();
            return services;
        }
    }
}
