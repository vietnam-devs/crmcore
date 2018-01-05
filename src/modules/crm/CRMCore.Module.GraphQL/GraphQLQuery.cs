using CRMCore.Module.GraphQL.Models;
using CRMCore.Module.GraphQL.Resolvers;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace CRMCore.Module.GraphQL
{
    public class GraphQLQuery : ObjectGraphType<object>
    {
        private IDatabaseMetadata _dbMetadata;
        private DbContext _dbContext;

        public GraphQLQuery(DbContext dbContext, IDatabaseMetadata dbMetadata)
        {
            _dbMetadata = dbMetadata;
            _dbContext = dbContext;

            Name = "Query";

            foreach (var metaTable in _dbMetadata.GetMetadataTables())
            {
                var tableType = new TableType(metaTable);
                AddField(new FieldType
                {
                    Name = metaTable.TableName,
                    Type = tableType.GetType(),
                    ResolvedType = tableType,
                    Resolver = new MyFieldResolver(metaTable, _dbContext),
                    Arguments = new QueryArguments(
                        tableType.TableArgs
                    )
                });

                // lets add key to get list of current table
                var listType = new ListGraphType(tableType);
                AddField(new FieldType
                {
                    Name = $"{metaTable.TableName}_list",
                    Type = listType.GetType(),
                    ResolvedType = listType,
                    Resolver = new MyFieldResolver(metaTable, _dbContext),
                    Arguments = new QueryArguments(
                        tableType.TableArgs                        
                    )
                });
            }
        }
    }
}
