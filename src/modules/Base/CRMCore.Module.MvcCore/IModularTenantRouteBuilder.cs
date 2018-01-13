using Microsoft.AspNetCore.Routing;

namespace CRMCore.Module.MvcCore
{
    public interface IModularTenantRouteBuilder
    {
        IRouteBuilder Build();
        void Configure(IRouteBuilder builder);
    }
}
