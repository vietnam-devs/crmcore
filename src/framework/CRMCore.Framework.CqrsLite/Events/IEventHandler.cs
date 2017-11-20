using CRMCore.Framework.CqrsLite.Messages;

namespace CRMCore.Framework.CqrsLite.Events
{
    /// <summary>
    /// Defines a handler for an event.
    /// </summary>
    /// <typeparam name="T">Event type being handled</typeparam>
    public interface IEventHandler<in T> : IHandler<T> where T : IEvent
    {
    }
}
