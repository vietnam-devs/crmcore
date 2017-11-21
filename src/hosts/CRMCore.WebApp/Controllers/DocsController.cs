using Microsoft.AspNetCore.Mvc;

namespace CRMCore.WebApp.Controllers
{
    // TODO: temporary put here 
    // TODO: ask aPhuong how to make razor view understand in module
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DocsController : Controller
    {
        [HttpGet]
        [Route("docs/")]
        public IActionResult Docs()
        {
            var vm = new DocsViewModel { Specification = "~/swagger/v1/swagger.json" };

            return View(nameof(Docs), vm);
        }
    }
}
