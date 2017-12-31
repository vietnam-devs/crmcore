using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using CRMCore.Module.CustomCollection.Extensions;

namespace CRMCore.Module.CustomCollection.Entity.Content
{
    public sealed class ContentFieldData : Dictionary<string, JToken>, IEquatable<ContentFieldData>
    {
        private static readonly JTokenEqualityComparer JTokenEqualityComparer = new JTokenEqualityComparer();

        public Guid Id { get; set; }

        public ContentFieldData()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        public ContentFieldData AddValue(string key, JToken value)
        {
            this[key] = value;

            return this;
        }

        public ContentFieldData AddValue(JToken value)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ContentFieldData);
        }

        public bool Equals(ContentFieldData other)
        {
            return other != null && (ReferenceEquals(this, other) || this.EqualsDictionary(other, EqualityComparer<string>.Default, JTokenEqualityComparer));
        }

        public override int GetHashCode()
        {
            return this.DictionaryHashCode(EqualityComparer<string>.Default, JTokenEqualityComparer);
        }
    }
}
