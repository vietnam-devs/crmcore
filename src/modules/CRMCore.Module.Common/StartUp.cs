using System;
using CRMCore.Mvc.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace CRMCore.Module.Common
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
        }

        public override void Configure(IApplicationBuilder builder, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            //routes.MapAreaRoute(
            //    name: "Common",
            //    areaName: "CRMCore.Module.Common",
            //    template: "Common",
            //    defaults: new { controller = "Common", action = "Index" }
            //);
        }
    }
}
