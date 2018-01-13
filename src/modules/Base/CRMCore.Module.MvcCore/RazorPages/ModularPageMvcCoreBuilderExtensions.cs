using Microsoft.Extensions.DependencyInjection;

namespace CRMCore.Module.MvcCore.RazorPages
{
    public static class ModularPageMvcCoreBuilderExtensions
    {
        public static IMvcBuilder AddModularRazorPages(this IMvcBuilder builder)
        {
            builder.AddRazorPagesOptions(options =>
            {
                options.RootDirectory = "/";
                options.Conventions.Add(new DefaultModularPageRouteModelConvention());
            });

            return builder;
        }
    }
}
