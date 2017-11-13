using CRMCore.Mvc.Core.LocationExpander;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace CRMCore.Mvc.Core.Extensions
{
    public static class ModularServiceCollectionExtensions
    {
        public static IServiceCollection AddMvcModules(this IServiceCollection services)
        {
            services.AddMvc()
                    .AddViewLocalization();
            AddModularRazorViewEngine(services);
            AddMvcModuleCoreServices(services);

            return services;
        }

        internal static void AddMvcModuleCoreServices(this IServiceCollection services)
        {
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
    }
}
