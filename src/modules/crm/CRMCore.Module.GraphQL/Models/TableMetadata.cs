using System.Collections.Generic;

namespace CRMCore.Module.GraphQL.Models
{
    public class TableMetadata
    {
        public string TableName { get; set; }

        public string AssemblyFullName { get; set; }

        public List<ColumnMetadata> Columns { get; set; }
    }
}
