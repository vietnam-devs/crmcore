using System;

namespace CRMCore.Module.Task.Features.GetTasks
{
    public class GetTaskResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DueType { get; set; }
        public Guid AssignedTo { get; set; }
        public string CategoryType { get; set; }
    }
}
