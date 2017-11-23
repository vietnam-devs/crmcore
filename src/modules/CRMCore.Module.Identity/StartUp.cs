using System;
using CRMCore.Framework.MvcCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using CRMCore.Module.Identity.Services;

namespace CRMCore.Module.Identity
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            // services.AddScoped<ILoginService, LoginService>();
        }

        public override void Configure(IApplicationBuilder builder, IRouteBuilder routes, IServiceProvider serviceProvider)
        {

        }
    }
}
