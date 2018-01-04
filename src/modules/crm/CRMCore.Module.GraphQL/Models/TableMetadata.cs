using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMCore.Module.GraphQL.Models
{
    public class TableMetadata
    {
        [Column("table_name")]
        public string TableName { get; set; }

        public List<ColumnMetadata> Columns { get; set; }
    }
}
