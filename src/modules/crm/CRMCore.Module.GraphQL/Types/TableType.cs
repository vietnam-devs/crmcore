using CRMCore.Module.GraphQL.Models;
using CRMCore.Module.GraphQL.Resolvers;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;

namespace CRMCore.Module.GraphQL.Types
{
    public class TableType : ObjectGraphType<object>
    {
        public TableType(TableMetadata tableMetadata)
        {
            Name = tableMetadata.TableName;
            foreach (var tableColumn in tableMetadata.Columns)
            {
                InitGraphTableColumn(tableColumn);
            }

            TableArgs.Add(new QueryArgument<IdGraphType> { Name = "id" });
            TableArgs.Add(new QueryArgument<IntGraphType> { Name = "first" });
            TableArgs.Add(new QueryArgument<IntGraphType> { Name = "offset" });
            TableArgs.Add(new QueryArgument<StringGraphType> { Name = "includes" });
        }

        public QueryArguments TableArgs
        {
            get; set;
        }

        private IDictionary<string, Type> _databaseTypeToSystemType;
        protected IDictionary<string, Type> DatabaseTypeToSystemType
        {
            get
            {
                if (_databaseTypeToSystemType == null)
                {
                    _databaseTypeToSystemType = new Dictionary<string, Type> {
                        { "uniqueidentifier", typeof(String) },
                        { "char", typeof(String) },
                        { "nvarchar", typeof(String) },
                        { "int", typeof(int) },
                        { "decimal", typeof(decimal) },
                        { "bit", typeof(bool) }
                    };
                }
                return _databaseTypeToSystemType;
            }
        }

        private void InitGraphTableColumn(ColumnMetadata columnMetadata)
        {
            var graphQLType = (ResolveColumnMetaType(columnMetadata.DataType)).GetGraphTypeFromType(true);
            var columnField = Field(
                graphQLType,
                columnMetadata.ColumnName
            );

            columnField.Resolver = new NameFieldResolver();
            FillArgs(columnMetadata.ColumnName);
        }

        private void FillArgs(string columnName)
        {
            if (TableArgs == null)
            {
                TableArgs = new QueryArguments(
                    new QueryArgument<StringGraphType>()
                    {
                        Name = columnName
                    }
                );
            }
            else
            {
                TableArgs.Add(new QueryArgument<StringGraphType> { Name = columnName });
            }
        }

        private Type ResolveColumnMetaType(string dbType)
        {
            if (DatabaseTypeToSystemType.ContainsKey(dbType))
                return DatabaseTypeToSystemType[dbType];

            return typeof(String);
        }
    }
}
