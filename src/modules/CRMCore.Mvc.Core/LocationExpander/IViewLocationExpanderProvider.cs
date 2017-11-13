using Microsoft.AspNetCore.Mvc.Razor;

namespace CRMCore.Mvc.Core.LocationExpander
{
    public interface IViewLocationExpanderProvider : IViewLocationExpander
    {
        int Priority { get; }
    }
}
