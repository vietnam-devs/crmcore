using CRMCore.Module.Data;
using CRMCore.Module.GraphQL.Models;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace CRMCore.Module.GraphQL
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMyGraphQL(this IServiceCollection services)
        {
            services.AddScoped<ITableNameLookup, TableNameLookup>();
            services.AddScoped<IDatabaseMetadata, DatabaseMetadata>();
            services.AddScoped((resolver) =>
            {
                var dbContext = resolver.GetRequiredService<ApplicationDbContext>();
                var metaDatabase = resolver.GetRequiredService<IDatabaseMetadata>();
                var tableNameLookup = resolver.GetRequiredService<ITableNameLookup>();

                var schema = new Schema { Query = new GraphQLQuery(dbContext, metaDatabase, tableNameLookup) };
                schema.Initialize();

                return schema;
            });

            return services;
        }
    }
}
