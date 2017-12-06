using CRMCore.Framework.Entities;
using CRMCore.Framework.MvcCore.Extensions;
using CRMCore.Module.Data;
using CRMCore.Module.Data.Impl;
using CRMCore.Module.Identity.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;

namespace CRMCore.Application.Crm.targets
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCrmCore(
            this IServiceCollection services, 
            IConfiguration config, 
            Action<DbContextOptionsBuilder> dbContextOptionsBuilderAction)
        {
            services.AddSingleton(JavaScriptEncoder.Default);

            services.AddDbContext<ApplicationDbContext>(options => dbContextOptionsBuilderAction(options));

            services.AddMvcModules();

            services.RegisterIdentityAndID4(
                config.GetSection("Certificate"),
                options =>
                {
                    options.ConfigureDbContext = builder =>
                        dbContextOptionsBuilderAction(builder);
                },
                options =>
                {
                    options.ConfigureDbContext = builder => dbContextOptionsBuilderAction(builder);
                });

            // TODO: I think this one should move to CRMCore.Module.Data :))
            var entityTypes = "CRMCore.Module.*".LoadAssemblyWithPattern()
                .SelectMany(m => m.DefinedTypes)
                .Where(x => typeof(IEntity)
                .IsAssignableFrom(x) && !x.GetTypeInfo().IsAbstract);

            foreach (var entity in entityTypes)
            {
                var repoType = typeof(IEfRepositoryAsync<>).MakeGenericType(entity);
                var implRepoType = typeof(EfRepositoryAsync<>).MakeGenericType(entity);
                services.AddSingleton(repoType, implRepoType);
            }

            services.AddSingleton(
                typeof(IUnitOfWorkAsync), resolver =>
                new EfUnitOfWork(
                    resolver.GetService<ApplicationDbContext>(),
                    resolver.GetService<IServiceProvider>()));

            return services;
        }
    }
}
