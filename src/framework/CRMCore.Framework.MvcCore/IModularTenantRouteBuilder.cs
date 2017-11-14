using Microsoft.AspNetCore.Routing;

namespace CRMCore.Framework.MvcCore
{
    public interface IModularTenantRouteBuilder
    {
        IRouteBuilder Build();

        void Configure(IRouteBuilder builder);
    }
}
