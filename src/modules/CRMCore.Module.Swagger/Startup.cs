using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using CRMCore.Framework.MvcCore;
using Swashbuckle.AspNetCore.Swagger;

namespace CRMCore.Module.Swagger
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
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

        public override void Configure(IApplicationBuilder builder, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            builder
                .UseSwagger()
                .UseSwaggerUI(
                    c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRM-Core API set.");
                    });
        }
    }
}