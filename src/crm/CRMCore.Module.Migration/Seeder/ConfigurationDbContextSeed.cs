using System.Collections.Generic;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Linq;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4;

namespace CRMCore.Module.Migration.Seeder
{
    public class ConfigurationDbContextSeed
    {
        public async System.Threading.Tasks.Task SeedAsync(ConfigurationDbContext context, IConfiguration configuration)
        {
            await context.Clients.AddRangeAsync(
                GetClients(configuration)
                    .Where(x => !context.Clients.Any(y => y.ClientId == x.ClientId))
                    .Select(x => x.ToEntity()));
            
            await context.IdentityResources.AddRangeAsync(
                GetResources()
                    .Where(x => !context.IdentityResources.Any(y => y.Name == x.Name))
                    .Select(x => x.ToEntity()));

            await context.ApiResources.AddRangeAsync(
                GetApis()
                    .Where(x=> !context.ApiResources.Any(y => y.Name == x.Name))
                    .Select(x=>x.ToEntity()));  
            
            await context.SaveChangesAsync();
        }

        private IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("Notifications", "Notifications Service"),
                new ApiResource("Contacts", "Contacts Service")
            };
        }

        private IEnumerable<IdentityResource> GetResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        private IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            var clientUrls = new Dictionary<string, string>();
            clientUrls.Add("SpaReactClient", configuration.GetValue<string>("Clients:SpaReactClient"));
            clientUrls.Add("SpaAngularClient", configuration.GetValue<string>("Clients:SpaAngularClient"));
            clientUrls.Add("CrmCoreApi", configuration.GetValue<string>("Clients:CrmCoreApi"));

            return new List<Client>()
            {
                new Client
                {
                    ClientId = "1a0e947d-99d7-4c6d-a2f3-a196a7da64a4",
                    ClientName = "CRM Core SPA-React OpenId Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris =           { $"{clientUrls["SpaReactClient"]}/callback" },
                    RequireConsent = false,
                    PostLogoutRedirectUris = { $"{clientUrls["SpaReactClient"]}/logout" },
                    AllowedCorsOrigins =     { $"{clientUrls["SpaReactClient"]}" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "Notifications",
                        "Contacts"
                    }    
                },
                new Client
                {
                    ClientId = "b45a3635-a865-4c48-944d-9c46d1009167",
                    ClientName = "CRM Core SPA-Angular OpenId Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris =           { $"{clientUrls["SpaAngularClient"]}/" },
                    RequireConsent = false,
                    PostLogoutRedirectUris = { $"{clientUrls["SpaAngularClient"]}/" },
                    AllowedCorsOrigins =     { $"{clientUrls["SpaAngularClient"]}" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "Notifications",
                        "Contacts"
                    }
                },
                new Client
                {
                    ClientId = "5b811d87-75e0-49af-ac1c-1fe7ebd73f60",
                    ClientName = "CRM Core API Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{clientUrls["CrmCoreApi"]}/swagger/o2c.html" },
                    PostLogoutRedirectUris = { $"{clientUrls["CrmCoreApi"]}/swagger/" },

                    AllowedScopes =
                    {
                        "Notifications",
                        "Contacts"
                    }
                }
            }; 
        }
    }
}
