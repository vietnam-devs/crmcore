using System;

namespace CRMCore.Module.Task.Domain
{
    [Flags]
    public enum TaskStatus
    {
        NotStarted = 1,
        InProgress = 2,
        Pending = 4,
        Done = 8
    }
}
