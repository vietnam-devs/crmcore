using CRMCore.Module.MvcCore.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace CRMCore.Module.MvcCore.Extensions
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