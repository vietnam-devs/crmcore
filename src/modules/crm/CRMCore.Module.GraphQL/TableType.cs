using CRMCore.Module.GraphQL.Models;
using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CRMCore.Module.GraphQL
{
    public class TableType : ObjectGraphType<IDictionary<string, object>>
    {
        public QueryArguments TableArgs
        {
            get; set;
        }

        private IDictionary<string, Type> _SqliteTypeToSystemType;
        protected IDictionary<string, Type> SqliteTypeToSystemType
        {
            get
            {
                if (_SqliteTypeToSystemType == null)
                {
                    _SqliteTypeToSystemType = new Dictionary<string, Type> {
                        {"uniqueidentifier", typeof(String) },
                        { "char", typeof(String) },
                        { "nvarchar", typeof(String) },
                        { "int", typeof(int) },
                        { "decimal", typeof(decimal) },
                        { "bit", typeof(bool) }
                    };
                }
                return _SqliteTypeToSystemType;
            }
        }

        public TableType(TableMetadata tableMetadata)
        {
            Name = tableMetadata.TableName;
            foreach (var tableColumn in tableMetadata.Columns)
            {
                InitGraphTableColumn(tableColumn);
            }
        }

        private void InitGraphTableColumn(ColumnMetadata columnMetadata)
        {
            var graphQLType = (ResolveColumnMetaType(columnMetadata.DataType)).GetGraphTypeFromType(true);
            var columnField = Field(
                graphQLType,
                columnMetadata.ColumnName
            );

            columnField.Resolver = new DictionaryNameFieldResolver();
            // FillArgs(columnMetadata.ColumnName);
        }

        /*private void FillArgs(string columnName)
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
        } */

        private Type ResolveColumnMetaType(string dbType)
        {
            if (SqliteTypeToSystemType.ContainsKey(dbType))
                return SqliteTypeToSystemType[dbType];

            return typeof(String);
        }
    }

    public class DictionaryNameFieldResolver : IFieldResolver
    {
        // private BindingFlags _flags = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;

        public object Resolve(ResolveFieldContext context)
        {
            var source = context.Source;

            if (source == null)
            {
                return null;
            }

            var value = (source as IDictionary<string, object>)[context.FieldAst.Name];

            if (value == null)
            {
                throw new InvalidOperationException($"Expected to find property {context.FieldAst.Name} on {context.Source.GetType().Name} but it does not exist.");
            }

            return value;
        }
    }
}
