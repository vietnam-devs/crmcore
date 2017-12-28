using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace CRMCore.Module.Identity.Extensions
{
    public static class IdentityExtensions
    {
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
