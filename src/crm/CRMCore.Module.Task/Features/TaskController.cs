using CRMCore.Framework.Entities;
using CRMCore.Framework.Entities.Helpers;
using CRMCore.Module.Data;
using CRMCore.Module.Data.Extensions;
using CRMCore.Module.Task.Features.CreateTask;
using CRMCore.Module.Task.Features.DeleteTask;
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
        private readonly IOptions<PaginationOption> _paginationOption;

        public TaskController(
            IUnitOfWorkAsync unitOfWork, 
            IEfQueryRepository<Domain.Task> taskQuery,
            IOptions<PaginationOption> paginationOption)
        {
            _taskRepository = unitOfWork.Repository<Domain.Task>() as IEfRepositoryAsync<Domain.Task>;
            _taskQuery = taskQuery;
            _paginationOption = paginationOption;
        }

        [HttpGet]
        public async Task<PaginatedItem<GetTaskResponse>> Get(GetTaskRequest request, [FromQuery]int page, [FromQuery] int? pageSize)
        {
            return await _taskQuery
                .Return<GetTaskResponse>()
                .Criterion(new Criterion(
                    page > 0 ? page : 1,
                    pageSize.HasValue ? pageSize.Value : _paginationOption.Value.PageSize,
                    _paginationOption.Value))
                .Projection(x => new GetTaskResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    DueType = x.DueType.ToString("D"),
                    AssignedTo = x.AssignedTo,
                    CategoryType = x.CategoryType.ToString("D"),
                    Status = x.TaskStatus.ToString("D"),
                    Created = x.Created,
                    Updated = x.Updated
                })
                .ComplexQueryAsync();
        }

        [HttpGet("{id:guid}")]
        public async Task<GetTaskResponse> Get(Guid id)
        {
            var response = await _taskQuery
                .Return<GetTaskResponse>(x => x.Id == id)
                .ComplexFindOneAsync();

            return new GetTaskResponse
            {
                Id = response.Id,
                Name = response.Name,
                DueType = response.DueType.ToString("D"),
                AssignedTo = response.AssignedTo,
                CategoryType = response.CategoryType.ToString("D"),
                Status = response.TaskStatus.ToString("D"),
                Created = response.Created,
                Updated = response.Updated
            };
        }

        [HttpPost]
        public async Task<AddTaskResponse> Add([FromBody] AddTaskRequest request)
        {
            var response = await _taskRepository.AddAsync(
                Domain.Task.CreateInstance(
                    request.Name,
                    (Domain.DueType)request.DueType,
                    request.AssignedTo,
                    (Domain.CategoryType)request.CategoryType
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
                .ChangeDueType((Domain.DueType)request.DueType)
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

        [HttpGet("due-types")]
        public IEnumerable<KeyValueObject<int>> GetDueTypes()
        {
            return EnumHelper.GetEnumKeyValue<Domain.DueType, int>();
        }

        [HttpGet("category-types")]
        public IEnumerable<KeyValueObject<int>> GetCategoryTypes()
        {
            return EnumHelper.GetEnumKeyValue<Domain.CategoryType, int>();
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
