using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CRMCore.Mvc.Core.Middlewares
{
    public class ModularTenantRouterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly Dictionary<string, RequestDelegate> _pipelines = new Dictionary<string, RequestDelegate>();

        //hard tenant, not support tenant now!!!
        private string _tenantName = "hard_teneant";

        public ModularTenantRouterMiddleware(
            RequestDelegate next,
            ILogger<ModularTenantRouterMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Begin Routing Request");
            }

            RequestDelegate pipeline;

            if (!_pipelines.TryGetValue(_tenantName, out pipeline))
            {
                // Building a pipeline can't be done by two requests
                lock (_pipelines)
                {
                    if (!_pipelines.TryGetValue(_tenantName, out pipeline))
                    {
                        pipeline = BuildTenantPipeline(httpContext.RequestServices);

                        _pipelines.Add(_tenantName, pipeline);
                    }
                }
            }

            await pipeline.Invoke(httpContext);
        }

        // Build the middleware pipeline for the current tenant
        public RequestDelegate BuildTenantPipeline(IServiceProvider serviceProvider)
        {
            var startups = serviceProvider.GetServices<IStartup>();

            // IStartup instances are ordered by module dependency with an Order of 0 by default.
            // OrderBy performs a stable sort so order is preserved among equal Order values.
            startups = startups.OrderBy(s => s.Order);

            var appBuilder = new ApplicationBuilder(serviceProvider);
            var routeBuilder = new RouteBuilder(appBuilder)
            {
                DefaultHandler = serviceProvider.GetRequiredService<MvcRouteHandler>()
            };
            foreach (var startup in startups)
            {
                startup.Configure(appBuilder, routeBuilder, serviceProvider);
            }


           var router = routeBuilder.Build();

            appBuilder.UseRouter(router);

            var pipeline = appBuilder.Build();

            return pipeline;
        }
    }
}
