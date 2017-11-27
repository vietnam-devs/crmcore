using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using CRMCore.Framework.Entities.Schema;

namespace CRMCore.Module.Schema.Dtos
{
    public class StringFieldPropertiesDto : FieldPropertiesBaseDto
    {
        public string DefaultValue { get; set; }

        public string Pattern { get; set; }

        public string PatternMessage { get; set; }

        public int? MinLength { get; set; }

        public int? MaxLength { get; set; }

        public string[] AllowedValues { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public StringFieldType FieldType { get; set; }

        public override FieldProperties ToProperties()
        {
            // TODO: consider to use schema then
            throw new NotImplementedException();
        }
    }
}
