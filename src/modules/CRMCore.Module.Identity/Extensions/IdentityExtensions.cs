using CRMCore.Framework.Entities.Identity;
using CRMCore.Module.Data;
using CRMCore.Module.Identity.Services;
using IdentityServer4.EntityFramework.Options;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace CRMCore.Module.Identity.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection RegisterIdentityAndID4(
            this IServiceCollection services,
            Action<ConfigurationStoreOptions> configurationstoreOptionsAction,
            Action<OperationalStoreOptions> operationalStoreOptionsAction)
        {
            // clear any handler for JWT
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Adds IdentityServer
            var identityServerBuilder = services.AddIdentityServer(x =>
            {
                x.IssuerUri = "null";
                x.UserInteraction.LoginUrl = "/identity/account/login";
                x.UserInteraction.ConsentUrl = "/identity/consent/index";
                x.UserInteraction.ErrorUrl = "/identity/home/error";
            })
            .AddAspNetIdentity<ApplicationUser>()
            .AddConfigurationStore(configurationstoreOptionsAction)
            .AddOperationalStore(operationalStoreOptionsAction)
            .AddProfileService<IdentityWithAdditionalClaimsProfileService>()
            .AddJwtBearerClientAuthentication()
            .AddAppAuthRedirectUriValidator();

            var env = services.BuildServiceProvider().GetService<IHostingEnvironment>();
            if (env.IsDevelopment())
            {
                identityServerBuilder.AddDeveloperSigningCredential();
            }
            else
            {
                var options = services.BuildServiceProvider().GetService<IConfiguration>().GetSection("Certificate");
                identityServerBuilder.AddCertificateFromFile(options);
            }

            // services.AddTransient<IRedirectUriValidator, CustomRedirectUriValidator>();

            return services;
        }

        public static IIdentityServerBuilder AddCertificateFromFile( this IIdentityServerBuilder builder, IConfigurationSection options)
        {
            var keyFilePath = options.GetValue<string>("FileName");
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
            }

            return builder;
        }
    }
}
