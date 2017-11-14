using System.Collections.Generic;
using CRMCore.Framework.MvcCore.Extensions;
using Microsoft.AspNetCore.Mvc.Razor;

namespace CRMCore.Framework.MvcCore.LocationExpander
{
    public class ModularViewLocationExpanderProvider : IViewLocationExpanderProvider
    {
        private readonly IExtensionManager _extensionManager;

        public ModularViewLocationExpanderProvider(IExtensionManager extensionManager)
        {
            _extensionManager = extensionManager;
        }

        public int Priority => 10;

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            // Get Extension, and then add in the relevant views.
            var extension = _extensionManager.GetExtension(context.AreaName);
            if (extension == null)
            {
                return viewLocations;
            }

            var result = new List<string>();

            var extensionViewsPath = '/' + extension.SubPath + "/Views";
            result.Add(extensionViewsPath + "/{1}/{0}" + RazorViewEngine.ViewExtension);
            result.Add(extensionViewsPath + "/Shared/{0}" + RazorViewEngine.ViewExtension);

            result.AddRange(viewLocations);

            return result;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {

        }
    }
}