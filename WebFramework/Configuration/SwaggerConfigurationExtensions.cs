using Common;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace WebFramework.Configuration
{
    public static class SwaggerConfigurationExtensions
    {
        public static void AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen();
        }

        public static void UseCustomSwagger(this IApplicationBuilder app)
        {

            app.UseSwagger();
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "Sharp API V1");
                option.RoutePrefix = string.Empty;
            });

        }

    }
}
