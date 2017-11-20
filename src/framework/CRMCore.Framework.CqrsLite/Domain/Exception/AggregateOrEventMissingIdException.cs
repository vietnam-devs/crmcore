using System;

namespace CRMCore.Framework.CqrsLite.Domain.Exception
{
    public class AggregateOrEventMissingIdException : System.Exception
    {
        public AggregateOrEventMissingIdException(Type aggregateType, Type eventType)
            : base($"An event of type {eventType.FullName} was tried to save from {aggregateType.FullName} but no id where set on either")
        { }
    }
}