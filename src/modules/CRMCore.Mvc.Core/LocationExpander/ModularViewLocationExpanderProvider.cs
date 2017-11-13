using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRMCore.Mvc.Core.LocationExpander
{
    public class ModularViewLocationExpanderProvider : IViewLocationExpanderProvider
    {
        public int Priority => 10;

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            var result = new List<string>();

            var extensionViewsPath = "./Packages/CRMCore.Module.Common" + "/Views";
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