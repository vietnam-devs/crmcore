using CRMCore.Framework.Entities;
using CRMCore.Framework.Entities.Helpers;
using CRMCore.Module.Data;
using CRMCore.Module.Task.Features.CreateTask;
using CRMCore.Module.Task.Features.DeleteTask;
using CRMCore.Module.Task.Features.GetTasks;
using CRMCore.Module.Task.Features.UpdateTask;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMCore.Module.Task.Features
{
    // [Authorize]
    [Area("CRMCore.Module.Task")]
    [Route("task-module/api/tasks")]
    public class TaskController : Controller
    {
        private readonly IEfRepositoryAsync<Domain.Task> _taskRepository;

        public TaskController(IUnitOfWorkAsync unitOfWork)
        {
            _taskRepository = unitOfWork.Repository<Domain.Task>() as IEfRepositoryAsync<Domain.Task>;
        }

        [HttpGet]
        public async Task<IEnumerable<GetTaskResponse>> Get(GetTaskRequest request)
        {
            var responses = await _taskRepository.ListAsync();
            return responses.Select(x => new GetTaskResponse {
                Id = x.Id,
                Name = x.Name,
                DueType = x.DueType.ToString("D"),
                AssignedTo = x.AssignedTo,
                CategoryType = x.CategoryType.ToString("D"),
                Status = x.TaskStatus.ToString("D"),
                Created = x.Created,
                Updated = x.Updated
            });
        }

        [HttpGet("{id:guid}")]
        public async Task<GetTaskResponse> Get(Guid id)
        {
            var response = await _taskRepository.GetByIdAsync(id);
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
            var oldOne = await _taskRepository.GetByIdAsync(id);
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
            var oldOne = await _taskRepository.GetByIdAsync(id);
            if (oldOne == null)
                throw new CoreException($"Could not delete item #{id}.");

            var result = await _taskRepository.DeleteAsync(oldOne);
            return new DeleteTaskResponse();
        }

        [HttpGet("due-types")]
        public IEnumerable<KeyValueResponse<int>> GetDueTypes()
        {
            return EnumHelper.GetEnumKeyValue<Domain.DueType, int>();
        }

        [HttpGet("category-types")]
        public IEnumerable<KeyValueResponse<int>> GetCategoryTypes()
        {
            return EnumHelper.GetEnumKeyValue<Domain.CategoryType, int>();
        }

        [HttpGet("assign-users")]
        public IEnumerable<KeyValueResponse<Guid>> GetAssignUsers() 
        {
            return new List<KeyValueResponse<Guid>> {
                new KeyValueResponse<Guid>{
                    Key = IdHelper.GenerateId("0fd266b3-4376-4fa3-9a35-aabe1d08043e"),
                    Value = "demouser@nomail.com"
                }
            };            
        }
    }

    public class EnumHelper
    {
        public static IEnumerable<KeyValueResponse<TKey>> GetEnumKeyValue<TEnum, TKey>()
        {
            var metas = GetMetadata<TEnum, TKey>();
            var results = metas.Item1.Zip(metas.Item2, (key, value) =>
                new KeyValueResponse<TKey>
                {
                    Key = key,
                    Value = value
                }
            );
            return results;
        }

        public static (IEnumerable<TKey>, IEnumerable<string>) GetMetadata<TEnum, TKey>()
        {
            var keyArray = (TKey[])Enum.GetValues(typeof(TEnum));
            var nameArray = Enum.GetNames(typeof(TEnum));

            IList<TKey> keys = new List<TKey>();
            foreach (TKey item in keyArray)
            {
                keys.Add(item);
            }

            IList<string> names = new List<string>();
            foreach (var item in nameArray)
            {
                names.Add(item);
            }

            return (keys, names);
        }
    }
}
