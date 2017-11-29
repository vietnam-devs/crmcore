using System;
using System.ComponentModel.DataAnnotations;

namespace CRMCore.Module.CustomCollection.Dtos
{
    public sealed class SchemaDto
    {
        public Guid Id { get; set; }

        [Required]
        [RegularExpression("^[a-z0-9]+(\\-[a-z0-9]+)*$")]
        public string Name { get; set; }

        [Required]
        public SchemaPropertiesDto Properties { get; set; }

        public bool IsPublished { get; set; }

        public int Version { get; set; }
    }
}
