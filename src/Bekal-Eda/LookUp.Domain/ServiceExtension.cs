
using FluentValidation;
using LookUp.Domain.Dtos;
using LookUp.Domain.Entities;
using LookUp.Domain.Validations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace User.Domain
{
    public static class ServiceExtension
    {
        public static string DefaultConnection { get; } = "User_Db_Conn";
        public static ConfigurationManager Configuration { get; set; }
        

        public static void AddDomainContext(this IServiceCollection services, ConfigurationManager configuration) 
        {
            Configuration = configuration;
            services.AddDbContext<LookUpDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(DefaultConnection));
            });
        }

        public static IServiceCollection AddValidator(this IServiceCollection services)
        {
            services
                .AddScoped<IValidator<AttributeDto>, AttributeCreateValidator>()
                .AddScoped<IValidator<AttributeExceptStatusDto>, AttributeUpdateValidator>()
                .AddScoped<IValidator<AttributeStatusDto>, AttributeChangeStatusValidator>();
            return services;
        }
    }
}
