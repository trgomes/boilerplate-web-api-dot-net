using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Serilog;
using Solution.Api.Configurations.Setup;
using Solution.Api.Filters;
using Solution.Api.StartupConfiguration;
using System;

namespace Solution.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = GetAppSettings(services);
            var defaultScheme = JwtBearerDefaults.AuthenticationScheme;

            // AutoMapper Settings
            services.AddAutoMapper(typeof(Startup));

            // Authorization
            services.AddAuthorizationService(appSettings, defaultScheme);

            // Swagger Config
            services.AddSwaggerSetup();

            // MediatR
            services.AddMediatR(typeof(Startup));

            // .NET Native DI Abstraction
            services.AddDependencyInjectionSetup(appSettings);

            // Automapper config
            services.AddAutoMapperSetup();

            services
                .AddMvc(options =>
                {
                    options.EnableEndpointRouting = false;
                    options.Filters.Add(new AuthorizeFilter(defaultScheme));
                    options.Filters.Add(typeof(CustomExceptionFilter));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);    

            services.AddHttpClient();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile($"logs/api.log.txt");

            var appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerSetup();
            }
            
            app.UseHsts();

            // global cors policy
            app.UseCors(builder => builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin()
            );
            
            //app.UseCors(x => x
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .SetIsOriginAllowed(origin => true) // allow any origin
            //    .AllowCredentials()); // allow credentials

            app.UseRouting();

            app.UseAuthorization();

            app.UseMvc();
        }

        private AppSettings GetAppSettings(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            return appSettingsSection.Get<AppSettings>();
        }


    }
}
