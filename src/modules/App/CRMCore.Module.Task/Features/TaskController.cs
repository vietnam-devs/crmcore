using CRMCore.Module.Entities;
using CRMCore.Module.Entities.Helpers;
using CRMCore.Module.Data;
using CRMCore.Module.Data.Extensions;
using CRMCore.Module.Task.Features.CreateTask;
using CRMCore.Module.Task.Features.DeleteTask;
using CRMCore.Module.Task.Features.GetTask;
using CRMCore.Module.Task.Features.GetTasks;
using CRMCore.Module.Task.Features.UpdateTask;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRMCore.Module.Task.Features
{
    // [Authorize]
    [Area("CRMCore.Module.Task")]
    [Route("task-module/api/tasks")]
    public class TaskController : Controller
    {
        private readonly IEfRepositoryAsync<Domain.Task> _taskRepository;
        private readonly IEfQueryRepository<Domain.Task> _taskQuery;
        private readonly IGetTasksService _getTasksService;
        private readonly IOptions<PaginationOption> _paginationOption;

        public TaskController(
            IUnitOfWorkAsync unitOfWork, 
            IEfQueryRepository<Domain.Task> taskQuery,
            IGetTasksService getTasksService,
            IOptions<PaginationOption> paginationOption)
        {
            _taskRepository = unitOfWork.Repository<Domain.Task>() as IEfRepositoryAsync<Domain.Task>;
            _taskQuery = taskQuery;
            _getTasksService = getTasksService;
            _paginationOption = paginationOption;
        }

        [HttpGet("tasks-by-statuses")]
        public GetTasksResponse Get(GetTasksRequest request)
        {
            return _getTasksService.GetTaskByStatus();
        }

        [HttpGet("{id:guid}")]
        public async Task<GetTaskResponse> Get(Guid id)
        {
            var response = await _taskQuery.GetByIdAsync(id);
            return new GetTaskResponse
            {
                Id = response.Id,
                Name = response.Name,
                AssignedTo = response.AssignedTo,
                CategoryType = response.CategoryType,
                CategoryTypeString = response.CategoryType.ToString("D"),
                Status = response.TaskStatus,
                StatusString = response.TaskStatus.ToString("D"),
                LastUpdated = response.Updated == DateTime.MinValue
                    ? response.Created
                    : response.Updated
            };
        }

        [HttpPost]
        public async Task<AddTaskResponse> Add([FromBody] AddTaskRequest request)
        {
            var response = await _taskRepository.AddAsync(
                Domain.Task.CreateInstance(
                    request.Name,
                    request.AssignedTo
                    ));

            return new AddTaskResponse();
        }

        [HttpPut("{id:guid}")]
        public async Task<UpdateTaskResponse> Update(Guid id, [FromBody] UpdateTaskRequest request)
        {
            var oldOne = await _taskQuery.GetByIdAsync(id);
            if (oldOne == null)
                throw new CoreException($"Could not delete item #{id}.");

            oldOne.ChangeName(request.Name)
                .ChangeAssignedTo(request.AssignedTo)
                .ChangeCategoryType((Domain.CategoryType)request.CategoryType);

            var response = await _taskRepository.UpdateAsync(oldOne);
            return new UpdateTaskResponse();
        }

        [HttpDelete("{id:guid}")]
        public async Task<DeleteTaskResponse> Delete(Guid id)
        {
            var oldOne = await _taskQuery.GetByIdAsync(id);
            if (oldOne == null)
                throw new CoreException($"Could not delete item #{id}.");

            var result = await _taskRepository.DeleteAsync(oldOne);
            return new DeleteTaskResponse();
        }

        [HttpGet("category-types")]
        public IEnumerable<KeyValueObject<int>> GetCategoryTypes()
        {
            return EnumHelper.GetEnumKeyValue<Domain.CategoryType, int>();
        }

        [HttpGet("task-statuses")]
        public IEnumerable<KeyValueObject<int>> GetTaskStatuses()
        {
            return EnumHelper.GetEnumKeyValue<Domain.TaskStatus, int>();
        }

        [HttpGet("assign-users")]
        public IEnumerable<KeyValueObject<Guid>> GetAssignUsers() 
        {
            return new List<KeyValueObject<Guid>> {
                new KeyValueObject<Guid>(
                    key: IdHelper.GenerateId("0fd266b3-4376-4fa3-9a35-aabe1d08043e"),
                    value: "demouser@nomail.com"
                )
            };            
        }
    }
}
