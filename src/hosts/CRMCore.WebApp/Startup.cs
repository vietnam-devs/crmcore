using AspNetCore.RouteAnalyzer;
using CRMCore.Application.Crm.targets;
using CRMCore.Framework.MvcCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

            app.UseAuthentication();
            app.UseIdentityServer();

            app.UseCors("CorsPolicy");

            // override / route :((
            app.Use((context, next) =>
            {
                if (context.Request.Path.Value == "/")
                {
                    context.Request.Path = new PathString("/home");
                }
                return next();
            });

            app.UseMvc(routes =>
            {
                routes.MapRouteAnalyzer("/routes");

                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
            });

            // TODO: consider moving this up
            app.UseModules();
        }
    }
}
