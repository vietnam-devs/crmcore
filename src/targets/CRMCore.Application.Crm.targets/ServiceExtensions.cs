using CRMCore.Framework.Entities;
using CRMCore.Framework.MvcCore.Extensions;
using CRMCore.Module.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Text.Encodings.Web;

namespace CRMCore.Application.Crm.targets
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCrmCore(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var config = serviceProvider.GetService<IConfiguration>();
            var extendOptionsBuilder = serviceProvider.GetService<IExtendDbContextOptionsBuilder>();
            var dbConnectionStringFactory = serviceProvider.GetService<IDatabaseConnectionStringFactory>();

            services.AddSingleton(JavaScriptEncoder.Default);

            services.AddOptions()
                .Configure<PaginationOption>(config.GetSection("Pagination"));

            services.AddDbContext<ApplicationDbContext>(optionsBuilder => {
                var assemblyName = Assembly.GetEntryAssembly().GetName().Name;
                var connectionString = dbConnectionStringFactory.Create();
                extendOptionsBuilder.Extend(optionsBuilder, connectionString, assemblyName);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddMvcModules();

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

            // make the authentication working
            app.UseAuthentication();
            app.UseIdentityServer();

            app.UseCors("CorsPolicy");

            app.UseMvc(routes =>
            {
                preRouteAction(routes);

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // https://gist.github.com/int128/e0cdec598c5b3db728ff35758abdbafd
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    // spa.UseReactDevelopmentServer(npmScript: "watch");
                }
            });

            // TODO: consider moving this up
            app.UseModules();
            return app;
        }
    }
}
