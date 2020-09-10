using Common;
using Common.Utilities;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddDbContext<ApplicationDbContext>(
                    options => options.UseSqlServer("Data Source =.; Initial Catalog = SharpApiDb; Integrated Security = true")
                );
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));
            services.AddCustomIdentity(_SiteSettings.identitySettings);
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUnitOfWork, ApplicationDbContext>();
            services.AddJwtAuthentication(_SiteSettings.JwtSettings);    
            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

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
            app.UseMvc();

        }
    }

}

