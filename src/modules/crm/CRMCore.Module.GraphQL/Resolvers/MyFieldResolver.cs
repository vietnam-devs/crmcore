using CRMCore.Module.GraphQL.Models;
using GraphQL.Resolvers;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace CRMCore.Module.GraphQL.Resolvers
{
    public class MyFieldResolver : IFieldResolver
    {
        private TableMetadata _tableMetadata;
        private DbContext _dbContext;

        public MyFieldResolver(TableMetadata tableMetadata, DbContext dbContext)
        {
            _tableMetadata = tableMetadata;
            _dbContext = dbContext;
        }

        public object Resolve(ResolveFieldContext context)
        {
            var queryable = _dbContext.Query(_tableMetadata.AssemblyFullName);
            if (context.FieldName.Contains("_list"))
            {
                
                var first = context.Arguments["first"] != null ? 
                    context.GetArgument("first", int.MaxValue) :
                    int.MaxValue;

                var offset = context.Arguments["offset"] != null ? 
                    context.GetArgument("offset", 0) : 
                    0;

                return queryable
                    .Skip(offset)
                    .Take(first)
                    .ToDynamicList<object>();
            }
            else
            {
                var id = context.GetArgument<Guid>("id");
                return queryable.FirstOrDefault($"Id == @0", id);
            }
        }
    }
}
