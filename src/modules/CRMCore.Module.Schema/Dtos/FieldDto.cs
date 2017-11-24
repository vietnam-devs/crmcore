using System;
using System.ComponentModel.DataAnnotations;

namespace CRMCore.Module.Schema.Dtos
{
    public sealed class FieldDto
    {
        public Guid FieldId { get; set; }

        [Required]
        [RegularExpression("^[a-z0-9]+(\\-[a-z0-9]+)*$")]
        public string Name { get; set; }

        public bool IsHidden { get; set; }

        public bool IsLocked { get; set; }

        public bool IsDisabled { get; set; }

        [Required]
        public FieldPropertiesBaseDto Properties { get; set; }
    }
}
