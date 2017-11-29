using System;

namespace CRMCore.Module.CustomCollection.Entity.Content
{
    public sealed class NamedContentData : ContentData<string>, IEquatable<NamedContentData>
    {
        public NamedContentData()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        public NamedContentData MergeInto(NamedContentData target)
        {
            return Merge(this, target);
        }

        public NamedContentData ToCleaned()
        {
            return Clean(this, new NamedContentData());
        }

        public NamedContentData AddField(string name, ContentFieldData data)
        {
            this[name] = data;

            return this;
        }

        public bool Equals(NamedContentData other)
        {
            return base.Equals(other);
        }
    }
}
