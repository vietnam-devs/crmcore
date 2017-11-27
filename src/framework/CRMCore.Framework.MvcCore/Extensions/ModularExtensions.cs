using Microsoft.Extensions.DependencyModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CRMCore.Framework.MvcCore.Extensions
{
    public static class ModularExtensions
    {
        public static IEnumerable<Assembly> LoadAssemblyWithPattern(this string searchPattern)
        {
            var assemblies = new List<Assembly>();
            var searchRegex = new Regex(searchPattern, RegexOptions.IgnoreCase);
            var moduleAssemblyFiles = DependencyContext.Default.RuntimeLibraries.Where(x => searchRegex.IsMatch(x.Name)).ToList();

            foreach (var assemblyFiles in moduleAssemblyFiles)
            {
                assemblies.Add(Assembly.Load(new AssemblyName(assemblyFiles.Name)));
            }

            return assemblies;
        }
    }
}
