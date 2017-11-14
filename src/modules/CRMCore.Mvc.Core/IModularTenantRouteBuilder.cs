using System;
using Microsoft.AspNetCore.Routing;

namespace CRMCore.Mvc.Core
{
    public interface IModularTenantRouteBuilder
    {
        IRouteBuilder Build();

        void Configure(IRouteBuilder builder);
    }
}
