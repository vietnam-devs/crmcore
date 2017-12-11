using CRMCore.Module.Task.Features.GetTask;
using System.Collections.Generic;

namespace CRMCore.Module.Task.Features.GetTasks
{
    public class GetTasksResponse
    {
        public GetTasksResponse()
        {
            // NotStartedTasks = new List<GetTaskResponse>();
            // InProgressTasks = new List<GetTaskResponse>();
            // PendingTasks = new List<GetTaskResponse>();
            // DoneTasks = new List<GetTaskResponse>();
            Tasks = new List<GetTaskResponse>();
        }

        public List<GetTaskResponse> Tasks { get; set; }
        // public List<GetTaskResponse> NotStartedTasks { get; set; }
        // public List<GetTaskResponse> InProgressTasks { get; set; }
        // public List<GetTaskResponse> PendingTasks { get; set; }
        // public List<GetTaskResponse> DoneTasks { get; set; }
    }
}
