using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;

namespace CRMCore.Module.Swagger
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMySwagger(this IServiceCollection services)
        {
            var config = services.BuildServiceProvider().GetService<IConfiguration>();
            var uri = config.GetSection("ClientUris")["WebApiAndAuthUri"];

            return services.AddSwaggerGen(options =>
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
                    TokenUrl = $"{uri}/connect/token",
                    AuthorizationUrl = $"{uri}/connect/authorize",
                    Scopes = new Dictionary<string, string>
                    {
                        {"Notifications", "Notifications"},
                        {"Contacts", "Contacts"}
                    }
                });
            });
        }
    }
}
