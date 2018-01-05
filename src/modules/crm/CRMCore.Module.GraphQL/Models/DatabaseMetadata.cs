using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Linq;

namespace CRMCore.Module.GraphQL.Models
{
    public class DatabaseMetadata : IDatabaseMetadata
    {
        protected DbContext _dbContext;

        public DatabaseMetadata(DbContext dbContext)
        {
            _dbContext = dbContext;
            DatabaseName = _dbContext.Database.GetDbConnection().Database;
            if (Tables == null)
                ReloadMetadata();
        }

        public string DatabaseName { get; set; }

        public List<TableMetadata> Tables { get; set; }

        public IEnumerable<TableMetadata> GetMetadataTables()
        {
            if (Tables == null)
                return new List<TableMetadata>();

            return Tables;
        }

        public void ReloadMetadata()
        {
            Tables = FetchTableMetaData();
        }

        private List<TableMetadata> FetchTableMetaData()
        {
            var metaTables = new List<TableMetadata>();
            foreach (var entityType in _dbContext.Model.GetEntityTypes())
            {
                var metaTable = new TableMetadata();
                var relational = entityType.Relational();
                var tableName = relational.TableName;

                metaTable.TableName = tableName;
                metaTable.AssemblyFullName = entityType.ClrType.FullName;
                metaTable.Columns = GetColumnsMetadata(entityType).ToList();

                metaTables.Add(metaTable);
            }
                
            return metaTables;
        }

        private IEnumerable<ColumnMetadata> GetColumnsMetadata(IEntityType entityType)
        {
            var tableColumns = new List<ColumnMetadata>();

            foreach (var propertyType in entityType.GetProperties())
            {
                var relational = propertyType.Relational();
                tableColumns.Add(new ColumnMetadata
                {
                    ColumnName = relational.ColumnName,
                    DataType = relational.ColumnType
                });
            }

            return tableColumns;
        }
    }

    public interface IDatabaseMetadata
    {
        void ReloadMetadata();
        IEnumerable<TableMetadata> GetMetadataTables();
    }
}
