using System;

namespace CRMCore.Module.Task.Features.GetTask
{
    public class GetTaskResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AssignedTo { get; set; }
        public Domain.CategoryType CategoryType { get; set; }
        public string CategoryTypeString { get; set; }
        public Domain.TaskStatus Status { get; set; }
        public string StatusString { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
