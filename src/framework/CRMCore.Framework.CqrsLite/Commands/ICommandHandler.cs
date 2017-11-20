using CRMCore.Framework.CqrsLite.Messages;

namespace CRMCore.Framework.CqrsLite.Commands
{
    /// <summary>
    /// Defines a handler for a command.
    /// </summary>
    /// <typeparam name="T">Event type being handled</typeparam>
    public interface ICommandHandler<in T> : IHandler<T> where T : ICommand
    {
    }
}