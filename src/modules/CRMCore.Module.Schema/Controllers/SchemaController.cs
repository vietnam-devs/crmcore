using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CRMCore.Module.Schema.Controllers
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
        public IEnumerable<Domain.Schema> Get()
        {
            return _readmodel.GetSchemaItems();
        }
    }
}
