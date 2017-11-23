using System;

namespace CRMCore.Module.Schema.Model
{
    public abstract class Field<T> : Field where T : FieldProperties, new()
    {
        private T properties;

        public T Properties
        {
            get { return properties; }
        }

        public override FieldProperties RawProperties
        {
            get { return properties; }
        }

        protected Field(Guid id, string name, T properties)
            : base(id, name)
        {
            this.properties = properties;
        }

        public override void Update(FieldProperties newProperties)
        {
            var typedProperties = ValidateProperties(newProperties);

            properties = typedProperties;
        }

        private T ValidateProperties(FieldProperties newProperties)
        {
            if (!(newProperties is T typedProperties))
            {
                throw new ArgumentException($"Properties must be of type '{typeof(T)}", nameof(newProperties));
            }

            return typedProperties;
        }
    }
}