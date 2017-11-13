using System;
using CRMCore.Mvc.Core.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CRMCore.Mvc.Core.Extensions
{
    public static class ModularApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseModules(this IApplicationBuilder app)
        {
            app.UseMiddleware<ModularTenantRouterMiddleware>();
            return app;
        }
    }
}