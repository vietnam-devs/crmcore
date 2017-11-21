namespace CRMCore.Module.Schema.Domain
{
    public enum StringFieldType
    {
        Input,
        Dropdown,
        Radio,
        RichText,
        TextArea
    }

    public class StringFieldProperties : FieldProperties
    {
        public int? MinLength { get; set; }

        public int? MaxLength { get; set; }

        public string DefaultValue { get; set; }

        public string Pattern { get; set; }

        public string PatternMessage { get; set; }

        public string[] AllowedValues { get; set; }

        public StringFieldType FieldType { get; set; }
    }
}
