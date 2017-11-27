using System;

namespace CRMCore.Framework.Entities
{
    public interface IEntity { }

    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}
