﻿
using LookUp.Domain.Entities;
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
    }
}
