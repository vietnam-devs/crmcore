using CRMCore.Framework.CqrsLite.Messages;
using System;

namespace CRMCore.Framework.CqrsLite.Events
{
    /// <summary>
    /// Defines an event with required fields.
    /// </summary>
    public interface IEvent : IMessage
    {
        Guid Id { get; set; }
        int Version { get; set; }
        DateTimeOffset TimeStamp { get; set; }
    }
}
