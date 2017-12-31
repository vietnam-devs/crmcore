using CRMCore.Module.Task.Features.GetTask;
using System.Collections.Generic;

namespace CRMCore.Module.Task.Features.GetTasks
{
    public class GetTasksResponse
    {
        public GetTasksResponse()
        {
            Tasks = new List<GetTaskResponse>();
        }

        public List<GetTaskResponse> Tasks { get; set; }
    }
}
