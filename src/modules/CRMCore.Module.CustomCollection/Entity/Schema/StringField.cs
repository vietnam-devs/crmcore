using System;

namespace CRMCore.Module.CustomCollection.Entity.Schema
{
    public class StringField : Field<StringFieldProperties>
    {
        /// <summary>
        /// This constructor for JsonSerializer only.
        /// </summary>
        internal StringField() : this(Guid.NewGuid(), "")
        { }
        
        public StringField(Guid id, string name)
            : base(id, name, new StringFieldProperties())
        {
        }

        public StringField(Guid id, string name, StringFieldProperties properties)
            : base(id, name, properties)
        {
        }
    }
}
