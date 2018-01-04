using System.ComponentModel.DataAnnotations.Schema;

namespace CRMCore.Module.GraphQL.Models
{
    public class ColumnMetadata
    {
        [Column("name")]
        public string ColumnName
        {
            get; set;
        }

        [Column("type")]
        public string DataType
        {
            get; set;
        }

        [Column("notnull")]
        public string IsNullable
        {
            get; set;
        }
    }
}
