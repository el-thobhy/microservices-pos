
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Domain.Dtos;
using User.Domain.Entities;
using User.Domain.Validations;

namespace User.Domain
{
    public static class ServiceExtension
    {
        public static string DefaultConnection { get; } = "User_Db_Conn";
        public static ConfigurationManager Configuration { get; set; }
        

        public static void AddDomainContext(this IServiceCollection services, ConfigurationManager configuration) 
        {
            Configuration = configuration;
            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(DefaultConnection));
            });
        }

        public static IServiceCollection AddValidator(this IServiceCollection services)
        {
            services.AddScoped<IValidator<UserDto>, UserCreateValidator>();
            return services;
        }
    }
}
