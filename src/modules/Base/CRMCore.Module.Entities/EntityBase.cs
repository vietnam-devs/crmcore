using CRMCore.Module.Entities.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRMCore.Module.Entities
{
    public interface IEntity : IIdentity
    { }

    public abstract class EntityBase : IEntity
    {
        protected List<IDomainEvent> Events = new List<IDomainEvent>();

        protected EntityBase() : this(IdHelper.GenerateId())
        {
        }

        protected EntityBase(Guid id)
        {
            Id = id;
            Created = DateTimeHelper.GenerateDateTime();
        }

        [Key]
        public Guid Id { get; protected set; }

        public DateTime Created { get; protected set; }

        public DateTime Updated { get; protected set; }

        public List<IDomainEvent> GetEvents()
        {
            return Events;
        }

        public EntityBase RemoveEvent(IDomainEvent @event)
        {
            if (Events.Find(e => e == @event) != null)
            {
                Events.Remove(@event);
            }
            return this;
        }

        public EntityBase RemoveAllEvents()
        {
            Events = new List<IDomainEvent>();
            return this;
        }
    }
}