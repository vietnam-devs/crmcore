using CRMCore.Module.Data;
using CRMCore.Module.GraphQL.Models;
using GraphQL.Resolvers;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CRMCore.Module.GraphQL
{
    public class GraphQLQuery : ObjectGraphType<object>
    {
        private IDatabaseMetadata _dbMetadata;
        private ApplicationDbContext _dbContext;

        public GraphQLQuery(ApplicationDbContext dbContext, IDatabaseMetadata dbMetadata)
        {
            _dbMetadata = dbMetadata;
            _dbContext = dbContext;

            Name = "Query";

            foreach (var metaTable in _dbMetadata.GetMetadataTables())
            {
                var tableType = new TableType(metaTable);
                /*AddField(new FieldType()
                {
                    Name = metaTable.TableName,
                    Type = tableType.GetType(),
                    ResolvedType = tableType,
                    Resolver = new MyFieldResolver(metaTable, _dbContext),
                    Arguments = new QueryArguments(
                        tableType.TableArgs
                    )
                });*/

                //lets add key to get list of current table
                var listType = new ListGraphType(tableType);
                AddField(new FieldType
                {
                    Name = $"{metaTable.TableName}_list",
                    Type = listType.GetType(),
                    ResolvedType = listType,
                    Resolver = new MyFieldResolver(metaTable, _dbContext),
                    /*Arguments = new QueryArguments(
                        tableType.TableArgs
                    ) */
                });
            }
        }
    }

    public class MyFieldResolver : IFieldResolver
    {
        private TableMetadata _tableMetadata;
        private ApplicationDbContext _dbContext;

        public MyFieldResolver(TableMetadata tableMetadata, ApplicationDbContext dbContext)
        {
            _tableMetadata = tableMetadata;
            _dbContext = dbContext;
        }

        public object Resolve(ResolveFieldContext context)
        {
            var source = context.Source;

            List<Dictionary<string, object>> finalResult = new List<Dictionary<string, object>>();
            if (context.FieldName.Contains("_list"))
            {

                using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "SELECT * FROM " + _tableMetadata.TableName;
                    _dbContext.Database.OpenConnection();
                    using (var result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            var temp = new Dictionary<string, object>();

                            for (var index = 0; index < result.FieldCount; index++)
                            {
                                var lowerCase = Char.ToLowerInvariant(result.GetName(index)[0]) + result.GetName(index).Substring(1);
                                temp.Add(lowerCase, result[result.GetName(index)]);
                            }

                            finalResult.Add(temp);
                        }
                    }

                    if (_dbContext.Database.GetDbConnection().State == System.Data.ConnectionState.Open)
                    {
                        _dbContext.Database.CloseConnection();
                    }
                }
            }

            return finalResult;
        }
    }
}
