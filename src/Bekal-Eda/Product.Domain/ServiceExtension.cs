using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain
{
    public static class ServiceExtension
    {
        public static string DefaultConnection { get; } = "Product_Db_Conn";
        public static string DefaultGetConnection { get; } = "Product_Get_Db_Conn";
        public static ConfigurationManager Configuration { get; set; }


        public static void AddDomainContext(this IServiceCollection services, ConfigurationManager configuration)
        {
            Configuration = configuration;
            services.AddDbContext<ProductDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(DefaultConnection));
            });
        }
        public static void AddGetDomainContext(this IServiceCollection services, ConfigurationManager configuration)
        {
            Configuration = configuration;
            services.AddDbContext<ProductDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(DefaultGetConnection));
            });
        }
    }
}
