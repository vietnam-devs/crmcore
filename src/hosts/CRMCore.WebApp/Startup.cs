using AspNetCore.RouteAnalyzer;
using CRMCore.Application.Crm.targets;
using CRMCore.Module.Data.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

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
                .AddSqlServerConfiguration()
                .AddCrmCore()
                .AddRouteAnalyzer()
                .BuildServiceProvider(false);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCrmCore(preRouteAction: routes => {
                routes.MapRouteAnalyzer("/routes");
            });
        }
    }
}
