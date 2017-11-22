using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using CRMCore.WebApp.Config;
using CRMCore.Framework.MvcCore.Extensions;
using Microsoft.Extensions.Configuration;
using System.Text.Encodings.Web;
using AspNetCore.RouteAnalyzer;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using CRMCore.Module.Data;
using CRMCore.Framework.Entities.Models;
using System.Reflection;

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

        public Startup(IHostingEnvironment env)
        {
            Environment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);


            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        private IHostingEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(Configuration.GetConnectionString("Default")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton(JavaScriptEncoder.Default);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddMvcModules();
            services.AddRouteAnalyzer();

            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info
                {
                    Title = "CRM-Core",
                    Version = "v1",
                    Description = "CRM-Core API set."
                });
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            MapAndUseIdSrv(app);
            MapAndUseWebApp(app);
            MapAndUseFrontend(app);

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

                app.UseSwagger().UseSwaggerUI(
                    c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRM-Core API set.");
                    });

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
