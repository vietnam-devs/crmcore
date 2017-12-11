using AspNetCore.RouteAnalyzer;
using CRMCore.Application.Crm.targets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace CRMCore.WebApp
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        private IHostingEnvironment Environment { get; }

        public Startup(IHostingEnvironment env, IConfiguration config)
        {
            Environment = env;
            Configuration = config;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services
                .AddCrmCore(
                    Configuration, 
                    builder => builder.UseMySql(
                        Configuration.GetConnectionString("Default"), 
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(
                                typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        }))
                .AddRouteAnalyzer()
                .BuildServiceProvider(false);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCrmCore(routes =>
            {
                routes.MapRouteAnalyzer("/routes");
            });
        }
    }
}
