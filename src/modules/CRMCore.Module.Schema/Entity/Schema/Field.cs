using CRMCore.Module.Schema.Converters;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace CRMCore.Framework.Entities.Schema
{
    [JsonConverter(typeof(JsonInheritanceConverter), "fieldType")]
    [KnownType(typeof(StringField))]
    public abstract class Field
    {
        private readonly Guid fieldId;
        private readonly string fieldName;
        private bool isDisabled;
        private bool isHidden;
        private bool isLocked;

        public Guid Id
        {
            get { return fieldId; }
        }

        public string Name
        {
            get { return fieldName; }
        }

        public bool IsLocked
        {
            get { return isLocked; }
        }

        public bool IsHidden
        {
            get { return isHidden; }
        }

        public bool IsDisabled
        {
            get { return isDisabled; }
        }

        public abstract FieldProperties RawProperties { get; }

        protected Field(Guid id, string name)
        {
            fieldId = id;
            fieldName = name;
        }

        public void Lock()
        {
            isLocked = true;
        }

        public void Hide()
        {
            isHidden = true;
        }

        public void Show()
        {
            isHidden = false;
        }

        public void Disable()
        {
            isDisabled = true;
        }

        public void Enable()
        {
            isDisabled = false;
        }

        public abstract void Update(FieldProperties newProperties);
    }
}