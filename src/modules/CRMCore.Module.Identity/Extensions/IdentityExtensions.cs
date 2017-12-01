using CRMCore.Framework.Entities.Identity;
using CRMCore.Module.Data;
using CRMCore.Module.Identity.Services;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace CRMCore.Module.Identity.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection RegisterIdentityAndID4(
            this IServiceCollection services,
            IConfigurationSection options,
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
                x.UserInteraction.LoginUrl = "/identity/account/login";
                x.UserInteraction.ConsentUrl = "/identity/consent/index";

            })
            //.AddDeveloperSigningCredential()
            .AddCertificateFromFile(options)
            .AddAspNetIdentity<ApplicationUser>()
            .AddConfigurationStore(configurationstoreOptionsAction)
            .AddOperationalStore(operationalStoreOptionsAction)
            .AddProfileService<IdentityWithAdditionalClaimsProfileService>();

            return services;
        }

        public static IIdentityServerBuilder AddCertificateFromFile( this IIdentityServerBuilder builder, IConfigurationSection options)
        {
            /* var keyFilePath = options.GetValue<string>("FileName");
            var keyFilePassword = options.GetValue<string>("Password");

            if (File.Exists(keyFilePath))
            {
                // You can simply add this line in the Startup.cs if you don't want an extension. 
                // This is neater though ;)
                builder.AddSigningCredential(new X509Certificate2(keyFilePath, keyFilePassword));
            }
            else
            {
                throw new Exception($"SigninCredentialExtension cannot find key file {keyFilePath}");
            } */

            return builder;
        }
    }
}
