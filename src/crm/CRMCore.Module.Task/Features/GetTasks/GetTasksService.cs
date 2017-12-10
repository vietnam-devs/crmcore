using CRMCore.Module.Data;
using System.Linq;
using CRMCore.Framework.Entities;
using CRMCore.Module.Task.Features.GetTask;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using System;

namespace CRMCore.Module.Task.Features.GetTasks
{
    public interface IGetTasksService
    {
        GetTasksResponse GetTaskByStatus(int page = 1);
    }

    public class GetTasksService : QueryServiceBase, IGetTasksService
    {
        private readonly IEfQueryRepository<Domain.Task> _taskRepo;
        private readonly IOptions<PaginationOption> _paginationOption;

        public GetTasksService(
            IEfQueryRepository<Domain.Task> taskRepo, 
            IOptions<PaginationOption> paginationOption)
        {
            _taskRepo = taskRepo;
            _paginationOption = paginationOption;
        }

        public GetTasksResponse GetTaskByStatus(int page = 1)
        {
            var notStartedStatuses = GetStatuses(x => x.TaskStatus == Domain.TaskStatus.NotStarted, page);
            var inProgressStatuses = GetStatuses(x => x.TaskStatus == Domain.TaskStatus.InProgress, page);
            var pendingStatuses = GetStatuses(x => x.TaskStatus == Domain.TaskStatus.Pending, page);
            var doneStatuses = GetStatuses(x => x.TaskStatus == Domain.TaskStatus.Done, page);

            var result = notStartedStatuses
                .Union(inProgressStatuses)
                .Union(pendingStatuses)
                .Union(doneStatuses)
                .ToList();

            return new GetTasksResponse
            {
                NotStartedStatuses = result.Where(x => x.Status == Domain.TaskStatus.NotStarted).ToList(),
                InProgressStatuses = result.Where(x => x.Status == Domain.TaskStatus.InProgress).ToList(),
                PendingStatuses = result.Where(x => x.Status == Domain.TaskStatus.Pending).ToList(),
                DoneStatuses = result.Where(x => x.Status == Domain.TaskStatus.Done).ToList()
            };
        }

        private IQueryable<GetTaskResponse> GetStatuses(Expression<Func<Domain.Task, bool>> filter, int page)
        {
            return _taskRepo
                .Queryable()
                .Where(filter)
                .Skip(page)
                .Take(page * _paginationOption.Value.PageSize)
                .Select(x => new GetTaskResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Status = x.TaskStatus,
                    StatusString = x.TaskStatus.ToString("D"),
                    CategoryType = x.CategoryType,
                    CategoryTypeString = x.CategoryType.ToString("D"),
                    AssignedTo = x.AssignedTo,
                    LastUpdated = x.Updated == DateTime.MinValue ? x.Created : x.Updated
                });
        }
    }
}
