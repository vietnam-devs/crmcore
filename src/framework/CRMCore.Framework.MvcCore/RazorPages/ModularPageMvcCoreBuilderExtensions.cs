using System;
using Microsoft.Extensions.DependencyInjection;

namespace CRMCore.Framework.MvcCore.RazorPages
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
