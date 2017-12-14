using CRMCore.Framework.Entities;
using CRMCore.Framework.MvcCore.Extensions;
using CRMCore.Module.Data;
using CRMCore.Module.Identity.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Encodings.Web;

namespace CRMCore.Application.Crm.targets
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCrmCore(this IServiceCollection services,
            IConfiguration config,
            Action<DbContextOptionsBuilder> builderAction)
        {
            services.AddSingleton(JavaScriptEncoder.Default);

            services.AddOptions()
                .Configure<PaginationOption>(config.GetSection("Pagination"));

            services.AddDbContext<ApplicationDbContext>(options => builderAction(options));

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
                    options.ConfigureDbContext = builder => builderAction(builder);
                },
                options =>
                {
                    options.ConfigureDbContext = builder => builderAction(builder);
                });

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddGenericDataModule();
            return services;
        }

        public static IApplicationBuilder UseCrmCore(this IApplicationBuilder app, Action<IRouteBuilder> preRouteAction)
        {
            var env = app.ApplicationServices.GetService<IHostingEnvironment>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseAuthentication();
            app.UseIdentityServer();

            app.UseCors("CorsPolicy");

            // override / route :((
            app.Use(next => async context =>
            {
                if (context.Request.Path.Value == "/")
                {
                    context.Request.Path = new PathString("/home");
                }

                // let some other middlewares handle it
                await next.Invoke(context);
            });
            
            app.UseMvc(routes =>
            {
                preRouteAction(routes);

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // actually, we don't need this one :)
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "watch");
                }
            });

            // TODO: consider moving this up
            app.UseModules();
            return app;
        }
    }
}
