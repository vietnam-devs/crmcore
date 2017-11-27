using System;

namespace CRMCore.Framework.Entities
{
    /// <summary>
    /// https://eng.uber.com/schemaless-part-one/
    /// https://backchannel.org/blog/friendfeed-schemaless-mysql
    /// https://github.com/eklitzke/schemaless
    /// </summary>
    public abstract class BaseSchemalessEntity : BaseEntity
    {
        public string Body { get; set; }
    }

    public interface IEntity { }

    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}
