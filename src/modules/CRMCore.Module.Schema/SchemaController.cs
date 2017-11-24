using CRMCore.Module.Schema.Dtos;
using CRMCore.Module.Schema.ReadSide;
using CRMCore.Module.Schema.ReadSide.Impl;
using Microsoft.AspNetCore.Mvc;

namespace CRMCore.Module.Schema
{
    [Area("CRMCore.Module.Schema")]
    public class SchemaController : Controller
    {                                      
        private readonly IReadModelFacade _readmodel;

        public SchemaController()
        {
            _readmodel = new ReadModelFacade();
        }

        [HttpGet]
        [Route("schema/api/orgs/{org}/schemas")]
        [ProducesResponseType(typeof(SchemaDto[]), 200)]
        public IActionResult GetSchemas(string org)
        {
            return Ok(_readmodel.GetSchemaItems());
        }

        [HttpGet]
        [Route("schema/api/orgs/{org}/schemas/{name}/")]
        [ProducesResponseType(typeof(SchemaDetailsItemDto), 200)]
        public IActionResult GetSchema(string org, string name)
        {
            SchemaDetailsItemDto dto = _readmodel.GetSchemaDetailsItem(name);

            return Ok(dto);
        }
    }
}
