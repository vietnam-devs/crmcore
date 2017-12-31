namespace CRMCore.Module.CustomCollection.Entity.Schema
{
    public abstract class FieldProperties : NamedElementPropertiesBase
    {
        public bool IsRequired { get; set; }

        public bool IsListField { get; set; }

        public string Placeholder { get; set; }
    }
}
