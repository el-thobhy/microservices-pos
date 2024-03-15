using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payment.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain
{
    public static class ServiceExtension
    {
        public static string DefaultConnection { get; } = "Payment_Db_Conn";
        public static ConfigurationManager Configuration { get; set; }

        public static void AddDomainContext(this IServiceCollection services, ConfigurationManager configuration)
        {
            Configuration = configuration;
            services.AddDbContext<PaymentDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString(DefaultConnection));
            });
        }
    }
}
