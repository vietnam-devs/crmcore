using System;
using System.IdentityModel.Tokens.Jwt;
using CRMCore.Framework.Entities.Identity;
using CRMCore.Module.Data;
using CRMCore.Module.Identity.Services;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CRMCore.Module.Identity.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection RegisterIdentityAndID4(this IServiceCollection services,
                                                          Action<ConfigurationStoreOptions> configurationstoreOptionsAction,
                                                          Action<OperationalStoreOptions> operationalStoreOptionsAction)
        {
            // clear any handler for JWT
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Adds IdentityServer
            services.AddIdentityServer(x =>
            {
                x.IssuerUri = "null";
                x.UserInteraction.LoginUrl = "/identity/Login";
                x.UserInteraction.ConsentUrl = "/identity/consent";
            })
            .AddDeveloperSigningCredential()
                    .AddAspNetIdentity<ApplicationUser>()
                    .AddConfigurationStore(configurationstoreOptionsAction)
                    .AddOperationalStore(operationalStoreOptionsAction)
                    .AddProfileService<IdentityWithAdditionalClaimsProfileService>();

            return services;
        }
    }
}
