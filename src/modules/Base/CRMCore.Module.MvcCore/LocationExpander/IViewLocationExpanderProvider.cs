using Microsoft.AspNetCore.Mvc.Razor;

namespace CRMCore.Module.MvcCore.LocationExpander
{
    public interface IViewLocationExpanderProvider : IViewLocationExpander
    {
        int Priority { get; }
    }
}
