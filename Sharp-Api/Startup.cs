using Common;
using Data;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services;
using WebFramework.Configuration;
using WebFramework.Middlewares;

namespace Sharp_Api
{

    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly SiteSettings _SiteSettings;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _SiteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerServices();
            services.AddDbContext<ApplicationDbContext>(
                    options => options.UseSqlServer(_SiteSettings.DataBaseConectionString)
                );
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));
            services.AddCustomIdentity(_SiteSettings.identitySettings);
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUnitOfWork, ApplicationDbContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddJwtAuthentication(_SiteSettings.JwtSettings);
            services.AddHangfireServices(_SiteSettings);
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSeq();
            });

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomSwagger();
            app.UseCustomExceptionHandler();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler();
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseHangfireDashboard();

            app.UseMvc();


        }
    }

}

