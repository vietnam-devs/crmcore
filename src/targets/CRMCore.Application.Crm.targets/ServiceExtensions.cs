using CRMCore.Framework.Entities;
using CRMCore.Framework.MvcCore.Extensions;
using CRMCore.Module.Data;
using CRMCore.Module.Identity.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Encodings.Web;

namespace CRMCore.Application.Crm.targets
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCrmCore(
            this IServiceCollection services,
            IConfiguration config,
            Action<DbContextOptionsBuilder> dbContextOptionsBuilderAction)
        {
            services.AddSingleton(JavaScriptEncoder.Default);

            services.AddOptions()
                .Configure<PaginationOption>(config.GetSection("Pagination"));

            services.AddDbContext<ApplicationDbContext>(options => dbContextOptionsBuilderAction(options));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddMvcModules();

            services.RegisterIdentityAndID4(
                options =>
                {
                    options.ConfigureDbContext = builder =>
                        dbContextOptionsBuilderAction(builder);
                },
                options =>
                {
                    options.ConfigureDbContext = builder => dbContextOptionsBuilderAction(builder);
                });

            services.AddGenericDataModule();

            return services;
        }

        public static IApplicationBuilder UseCrmCore(this IApplicationBuilder app, Action<IRouteBuilder> moreRouteAction)
        {
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
                moreRouteAction(routes);
                

                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
            });

            // TODO: consider moving this up
            app.UseModules();

            return app;
        }
    }
}
