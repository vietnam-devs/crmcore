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
                .AddCrmCore(Configuration, builder => RegisterDbSettings(builder))
                .AddRouteAnalyzer()
                .BuildServiceProvider(false);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCrmCore(preRouteAction: routes => {
                routes.MapRouteAnalyzer("/routes");
            });
        }

        private void RegisterDbSettings(DbContextOptionsBuilder builder)
        {
            var assemblyName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            if (!Environment.IsDevelopment())
            {
                builder.UseMySql(
                   Configuration.GetConnectionString("Default"),
                   sqlOptions =>
                   {
                       sqlOptions.MigrationsAssembly(assemblyName);
                   });
            }
            else
            {
                builder.UseSqlServer(
                   Configuration.GetConnectionString("SqlServerDefault"),
                   sqlOptions =>
                   {
                       sqlOptions.MigrationsAssembly(assemblyName);
                   });
            }
        }
    }
}
