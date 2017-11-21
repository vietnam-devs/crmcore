using Microsoft.AspNetCore.Mvc;

namespace CRMCore.Module.ReDoc.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DocsController : Controller
    {
        // [HttpGet]
        // [Route("docs/")]
        public IActionResult Docs()
        {
            var vm = new DocsViewModel { Specification = "~/swagger/v1/swagger.json" };

            return View(nameof(Docs), vm);
        }
    }
}
