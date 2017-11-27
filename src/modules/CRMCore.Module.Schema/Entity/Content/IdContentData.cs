using System;
using System.Collections.Generic;

namespace CRMCore.Framework.Entities.Content
{
    public sealed class IdContentData : ContentData<long>, IEquatable<IdContentData>
    {
        public IdContentData()
            : base(EqualityComparer<long>.Default)
        {
        }

        public IdContentData(IdContentData copy)
            : base(copy, EqualityComparer<long>.Default)
        {
        }

        public IdContentData MergeInto(IdContentData target)
        {
            return Merge(this, target);
        }

        public IdContentData ToCleaned()
        {
            return Clean(this, new IdContentData());
        }

        public IdContentData AddField(long id, ContentFieldData data)
        {
            this[id] = data;

            return this;
        }

        public bool Equals(IdContentData other)
        {
            return base.Equals(other);
        }
    }
}
