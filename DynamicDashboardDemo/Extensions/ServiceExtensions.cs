using DNTCaptcha.Core;
using DynamicDashboardDemo.DataAccessLayer;
using DynamicDashboardDemo.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicDashboardDemo.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDashboardInfoRepo, DashboardInfo>();
            services.AddScoped<ILoginInfoRepo, LoginInfo>();
        }

        public static void AddCaptcha(this IServiceCollection services)
        {
            services.AddDNTCaptcha(options => options.UseCookieStorageProvider().ShowThousandsSeparators(false));
        }
    }
}
