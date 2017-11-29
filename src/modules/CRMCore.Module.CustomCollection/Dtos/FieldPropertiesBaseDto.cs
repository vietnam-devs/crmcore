using CRMCore.Module.CustomCollection.Converters;
using CRMCore.Module.CustomCollection.Entity.Schema;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CRMCore.Module.CustomCollection.Dtos
{
    [JsonConverter(typeof(JsonInheritanceConverter), "fieldType")]
    [KnownType(typeof(StringFieldPropertiesDto))]
    public abstract class FieldPropertiesBaseDto
    {
        [StringLength(50)]
        public string Label { get; set; }

        [StringLength(100)]
        public string Hints { get; set; }

        [StringLength(100)]
        public string Placeholder { get; set; }

        public bool IsRequired { get; set; }

        public bool IsListField { get; set; }

        public abstract FieldProperties ToProperties();
    }
}
