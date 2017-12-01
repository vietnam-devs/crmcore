using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using CRMCore.Module.Task.Features.GetTasks;
using CRMCore.Module.Data;
using CRMCore.Framework.Entities;
using System.Threading.Tasks;
using System.Linq;

namespace CRMCore.Module.Common.Controllers
{
    [Authorize]
    [Area("CRMCore.Module.Task")]
    [Route("task/api/[controller]")]
    public class TaskController : Controller
    {
        private readonly IEfRepositoryAsync<Task.Domain.Task> _taskRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public TaskController(IUnitOfWorkAsync unitOfWork)
        {
            _taskRepository = unitOfWork.Repository<Task.Domain.Task>() as IEfRepositoryAsync<Task.Domain.Task>;
        }

        [HttpGet]
        public async Task<IEnumerable<GetTaskResponse>> Get(GetTaskRequest request)
        {
            var responses = await _taskRepository.ListAsync();
            return responses.Select(x => new GetTaskResponse {
                Id = x.Id,
                Name = x.Name,
                DueType = x.DueType.ToString(),
                AssignedTo = x.AssignedTo,
                CategoryType = x.CategoryType.ToString()
            }); 
        }
    }
}
