using CRMCore.Module.Task.Features.GetTask;
using System.Collections.Generic;

namespace CRMCore.Module.Task.Features.GetTasks
{
    public class GetTasksResponse
    {
        public GetTasksResponse()
        {
            NotStartedStatuses = new List<GetTaskResponse>();
            InProgressStatuses = new List<GetTaskResponse>();
            PendingStatuses = new List<GetTaskResponse>();
            DoneStatuses = new List<GetTaskResponse>();
        }

        public List<GetTaskResponse> NotStartedStatuses { get; set; }
        public List<GetTaskResponse> InProgressStatuses { get; set; }
        public List<GetTaskResponse> PendingStatuses { get; set; }
        public List<GetTaskResponse> DoneStatuses { get; set; }
    }
}
