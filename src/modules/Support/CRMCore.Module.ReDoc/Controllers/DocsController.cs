using Microsoft.AspNetCore.Mvc;

namespace CRMCore.Module.ReDoc.Controllers
{
    [Area("CRMCore.Module.ReDoc")]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DocsController : Controller
    {
        public IActionResult Index()
        {
            var vm = new DocsViewModel { Specification = "~/my-swagger/v1/swagger.json" };

            return View(nameof(Index), vm);
        }
    }
}
