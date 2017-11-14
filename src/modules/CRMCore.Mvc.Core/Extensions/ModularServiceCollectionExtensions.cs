using System;
using System.Linq;
using CRMCore.Mvc.Core.LocationExpander;
using CRMCore.Mvc.Core.Options;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace CRMCore.Mvc.Core.Extensions
{
    public static class ModularServiceCollectionExtensions
    {
        public static IServiceCollection AddMvcModules(this IServiceCollection services)
        {
            services.AddExtensionLocation("packages");

            services.AddMvc().AddViewLocalization();
            AddModularRazorViewEngine(services);
            AddMvcModuleCoreServices(services);

            AddModule(services);

            return services;
        }

        internal static void AddMvcModuleCoreServices(this IServiceCollection services)
        {
            services.AddSingleton<IExtensionManager, ExtensionManager>();

            services.Replace(ServiceDescriptor.Scoped<IModularTenantRouteBuilder, ModularTenantRouteBuilder>());
            services.AddScoped<IViewLocationExpanderProvider, DefaultViewLocationExpanderProvider>();
            services.AddScoped<IViewLocationExpanderProvider, ModularViewLocationExpanderProvider>();
        }

        internal static void AddModularRazorViewEngine(this IServiceCollection services)
        {
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new CompositeViewLocationExpanderProvider());
            });
        }

        internal static IServiceCollection AddExtensionLocation(this IServiceCollection services, string subPath)
        {
            return services.Configure<ExtensionExpanderOptions>(configureOptions: options =>
            {
                options.Options.Add(new ExtensionExpanderOption { SearchPath = subPath.Replace('\\', '/').Trim('/') });
            });
        }

        internal static void AddModule(this IServiceCollection service)
        {
            var serviceProvider = service.BuildServiceProvider(true);
            var extensionManager = serviceProvider.GetService<IExtensionManager>();

            foreach (var dependency in extensionManager.GetExtensions()
                     .SelectMany(x => x.ExportedTypes)
                     .Where(t => typeof(IStartup).IsAssignableFrom(t)))
            {
                service.AddSingleton(typeof(IStartup), dependency);
            }

            var startups = serviceProvider.GetServices<IStartup>();

            // IStartup instances are ordered by module dependency with an Order of 0 by default.
            // OrderBy performs a stable sort so order is preserved among equal Order values.
            startups = startups.OrderBy(s => s.Order);

            // Let any module add custom service descriptors to the tenant
            foreach (var startup in startups)
            {
                startup.ConfigureServices(service);
            }

            (serviceProvider as IDisposable).Dispose();
        }
    }
}
