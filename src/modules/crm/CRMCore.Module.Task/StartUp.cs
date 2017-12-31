using CRMCore.Module.MvcCore;
using CRMCore.Module.Task.Features.GetTasks;
using CRMCore.Module.Task.Features.GetTasks.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CRMCore.Module.Task
{
    public class StartUp : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IGetTasksService, GetTasksService>();
        }

        public override void Configure(IApplicationBuilder builder, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
        }
    }
}
