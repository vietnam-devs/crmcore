using CRMCore.Framework.CqrsLite.Messages;

namespace CRMCore.Framework.CqrsLite.Commands
{
    /// <summary>
    /// Defines a handler for a command with a cancellation token.
    /// </summary>
    /// <typeparam name="T">Event type being handled</typeparam>
    public interface ICancellableCommandHandler<in T> : ICancellableHandler<T> where T : ICommand
    {
    }
}