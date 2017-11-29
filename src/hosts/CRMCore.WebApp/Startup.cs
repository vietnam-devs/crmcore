using AspNetCore.RouteAnalyzer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;

using CRMCore.Framework.MvcCore.Extensions;
using CRMCore.WebApp.Config;
using CRMCore.Module.Identity.Extensions;
using CRMCore.Module.Data;

namespace CRMCore.WebApp
{
    public class Startup
    {
        private static readonly string[] IdSrvPaths =
        {
            "/client-callback-popup",
            "/client-callback-silent",
            "/account",
            "/error"
        };

        private IConfiguration Configuration { get; }

        private IHostingEnvironment Environment { get; }

        public Startup(IHostingEnvironment env, IConfiguration config)
        {
            Environment = env;
            Configuration = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("Default");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddSingleton(JavaScriptEncoder.Default);

            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString));

            services.AddMvcModules();
            services.AddRouteAnalyzer();

            services.RegisterIdentityAndID4(
                options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseMySql(connectionString, sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(migrationsAssembly);
                        });
                },
                options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseMySql(connectionString, sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(migrationsAssembly);
                        });
                });
        }

        public void Configure(IApplicationBuilder app)
        {           
            app.UseStaticFiles();

            //Todo: need to refactor code, need to use authentication before MVC
            app.UseAuthentication();
            app.UseIdentityServer();

            MapAndUseIdSrv(app);
            MapAndUseWebApp(app);
            MapAndUseFrontend(app);

            // TODO: consider moving this up
            app.UseModules();
        }

        private void MapAndUseIdSrv(IApplicationBuilder app)
        {
            app.Map(Constants.IdentityPrefix, identityApp =>
            {
                if (Environment.IsDevelopment())
                {
                    identityApp.UseDeveloperExceptionPage();
                }
                else
                {
                    identityApp.UseExceptionHandler("/error");
                }

                // TODO: aPhuong will add more configurations for IdSrv 
                // TODO: ...

                /*identityApp.MapWhen(x => IsIdentityRequest(x), mvcApp =>
                {
                    mvcApp.UseMvc();
                });*/
            });
        }

        private void MapAndUseWebApp(IApplicationBuilder app)
        {
            app.Map(Constants.ApiPrefix, appApi =>
            {
                if (Environment.IsDevelopment())
                {
                    appApi.UseDeveloperExceptionPage();
                }
                else
                {
                    appApi.UseExceptionHandler("/Home/Error");
                }

                app.UseCors("CorsPolicy");

                /*appApi.MapWhen(x => !IsIdentityRequest(x), webApp =>
                {
                    // TODO: Thang will map Swagger here
                    // TODO: ...

                    webApp.UseMvc(routes =>
                        {
                            routes.MapRoute(
                            name: "default",
                            template: "{controller=Home}/{action=Index}/{id?}");

                            routes.MapSpaFallbackRoute(
                                name: "spa-fallback",
                                defaults: new { controller = "Home", action = "Index" });
                        });
                });*/
            });
        }

        private void MapAndUseFrontend(IApplicationBuilder app)
        {
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

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }

        private static bool IsIdentityRequest(HttpContext context)
        {
            return IdSrvPaths.Any(p => context.Request.Path.StartsWithSegments(p));
        }
    }
}
