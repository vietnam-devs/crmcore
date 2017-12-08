using CRMCore.Framework.Entities;
using CRMCore.Module.CustomCollection.Dtos;
using CRMCore.Module.CustomCollection.Entity;
using CRMCore.Module.Data;
using CRMCore.Module.Data.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CRMCore.Module.CustomCollection
{
    [Area("CRMCore.Module.CustomCollection")]
    public class CustomCollectionController : Controller
    {                                      
        private readonly IEfRepositoryAsync<Morphism> _morphismRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public CustomCollectionController(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _morphismRepository = _unitOfWork.Repository<Morphism>() as IEfRepositoryAsync<Morphism>;
        }

        [HttpGet]
        [Route("custom-collection/api/orgs/{org}/schemas")]
        [ProducesResponseType(typeof(SchemaDto[]), 200)]
        public async Task<IActionResult> GetSchemas(string org)
        {
            // return Ok(await _morphismRepository.ListAsync());
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("custom-collection/api/orgs/{org}/schemas/{name}/")]
        [ProducesResponseType(typeof(SchemaDetailsItemDto), 200)]
        public async Task<IActionResult> GetSchema(string org, string name)
        {
            // return Ok(await _morphismRepository.FindOneAsync(x => x.Name == name));
            throw new NotImplementedException();
        }
    }
}
