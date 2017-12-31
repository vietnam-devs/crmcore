using AspNetCore.RouteAnalyzer;
using CRMCore.Application.Crm.targets;
using CRMCore.Module.Data.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CRMCore.WebApp
{
    public class Startup
    {
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
