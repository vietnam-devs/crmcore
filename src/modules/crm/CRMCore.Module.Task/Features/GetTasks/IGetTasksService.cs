namespace CRMCore.Module.Task.Features.GetTasks
{
    public interface IGetTasksService
    {
        GetTasksResponse GetTaskByStatus(int page = 1);
    }
}
