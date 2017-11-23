using CRMCore.Module.Schema.Features.GetSchemaItems.Dtos;
using CRMCore.Module.Schema.ReadSide;
using CRMCore.Module.Schema.ReadSide.Impl;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CRMCore.Module.Schema
{
    [Area("CRMCore.Module.Schema")]
    [Route("schema/api/schemas")]
    public class SchemaController : Controller
    {                                      
        private readonly IReadModelFacade _readmodel;

        public SchemaController()
        {
            _readmodel = new ReadModelFacade();
        }

        [HttpGet]
        public IEnumerable<SchemaDto> Get()
        {
            return _readmodel.GetSchemaItems();
        }
    }
}
