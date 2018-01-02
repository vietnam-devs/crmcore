using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace CRMCore.Module.Swagger
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMySwagger(this IApplicationBuilder app)
        {
            var config = app.ApplicationServices.GetService(typeof(IConfiguration)) as IConfiguration;
            var uri = config.GetSection("ClientUris")["WebApiAndAuthUri"];

            return app
                .UseSwagger(c =>
                {
                    c.RouteTemplate = "my-swagger/{documentName}/swagger.json";
                })
                .UseSwaggerUI(
                    c =>
                    {
                        c.SwaggerEndpoint($"{uri}/my-swagger/v1/swagger.json", "CRM-Core API set.");
                        c.RoutePrefix = "my-swagger";
                        c.ConfigureOAuth2("5b811d87-75e0-49af-ac1c-1fe7ebd73f60", "", "", "CRM Core API Swagger UI");
                    });
        }
    }
}
