using Microsoft.AspNetCore.Mvc.Razor;

namespace CRMCore.Framework.MvcCore.LocationExpander
{
    public interface IViewLocationExpanderProvider : IViewLocationExpander
    {
        int Priority { get; }
    }
}
