using CRMCore.Module.MvcCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;

namespace CRMCore.Module.Swagger
{
    public class FooController : Controller
    {
        [SwaggerOperation(Tags = new[] { "foo-group" })]
        [HttpGet("foo/test")]
        public IActionResult Get()
        {
            return Ok("foo");
        }
    }

    public class BarController : Controller
    {
        [SwaggerOperation(Tags = new[] { "foo-group" })]
        [HttpGet("foo/bar/test")]
        public IActionResult Get()
        {
            return Ok("bar");
        }
    }

    public class Startup : StartupBase
    {
        public override int Order => 100;
        private string _uri = string.Empty;

        public Startup(IConfiguration config)
        {
            _uri = config.GetSection("ClientUris")["WebApiAndAuthUri"];
        }

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
                options.OperationFilter<AuthorizeCheckOperationFilter>();
                options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    TokenUrl = $"{_uri}/connect/token",
                    AuthorizationUrl = $"{_uri}/connect/authorize",
                    Scopes = new Dictionary<string, string>
                    {
                        {"Notifications", "Notifications"},
                        {"Contacts", "Contacts"}
                    }
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
                        c.SwaggerEndpoint($"{_uri}/swagger/v1/swagger.json", "CRM-Core API set.");
                        c.ConfigureOAuth2("5b811d87-75e0-49af-ac1c-1fe7ebd73f60", "", "", "CRM Core API Swagger UI");
                    });
        }
    }
}