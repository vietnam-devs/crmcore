using CRMCore.Framework.Entities.Identity;
using CRMCore.Framework.MvcCore;
using CRMCore.Module.Data;
using CRMCore.Module.Identity.Extensions;
using CRMCore.Module.Identity.Services;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace CRMCore.Module.Identity
{
    public class Startup : StartupBase
    {
        public override int Order
        {
            get
            {
                return 1;
            }
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ILoginService<ApplicationUser>, LoginService>();
            services.AddTransient<IProfileService, IdentityWithAdditionalClaimsProfileService>();

            var serviceProvider = services.BuildServiceProvider();
            var env = serviceProvider.GetService<IHostingEnvironment>();
            var config = serviceProvider.GetService<IConfiguration>();
            var extendOptionsBuilder = serviceProvider.GetService<IExtendDbContextOptionsBuilder>();
            var dbConnectionStringFactory = serviceProvider.GetService<IDatabaseConnectionStringFactory>();

            Action<DbContextOptionsBuilder> optionsBuilderAction =
                (optionsBuilder) =>
                {
                    var assemblyName = Assembly.GetEntryAssembly().GetName().Name;
                    var connectionString = dbConnectionStringFactory.Create();
                    extendOptionsBuilder.Extend(optionsBuilder, connectionString, assemblyName);
                };

            // Adds DbContexts
            services.AddDbContext<ApplicationDbContext>(options => optionsBuilderAction(options));
            services.AddIdentity<ApplicationUser, ApplicationRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();

            // Adds IdentityServer
            var identityServerBuilder = services
                .AddIdentityServer(x =>
                {
                    x.IssuerUri = "null";
                    x.UserInteraction.LoginUrl = "/identity/account/login";
                    x.UserInteraction.ConsentUrl = "/identity/consent/index";
                    x.UserInteraction.ErrorUrl = "/identity/home/error";
                })
                .AddAspNetIdentity<ApplicationUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder => optionsBuilderAction(builder);
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder => optionsBuilderAction(builder);
                })
                .AddProfileService<IdentityWithAdditionalClaimsProfileService>()
                .AddJwtBearerClientAuthentication()
                .AddAppAuthRedirectUriValidator();

            if (env.IsDevelopment())
            {
                identityServerBuilder.AddDeveloperSigningCredential();
            }
            else
            {
                var options = services.BuildServiceProvider().GetService<IConfiguration>().GetSection("Certificate");
                identityServerBuilder.AddCertificateFromFile(options);
            }

            base.ConfigureServices(services);
        }

        public override void Configure(IApplicationBuilder app, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            // TODO: aPhuong: could we make it load before the MvcCore?
            // app.UseAuthentication();
            // app.UseIdentityServer();
        }
    }
}
