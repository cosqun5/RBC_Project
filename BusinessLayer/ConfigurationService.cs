using Business.Profiles.EmployeeP;
using Business.Services.Abstract;
using Business.Services.Concrate;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public static class ConfigurationService
    {
        public static IServiceCollection AddBusinessConfiguration(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager configuration)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<EmployeeProfile>();
            });
            services.AddScoped<IEmployeeService, EmployeeService>();



            return services;
        }
    }
}
