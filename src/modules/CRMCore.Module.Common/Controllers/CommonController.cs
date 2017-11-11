using Microsoft.AspNetCore.Mvc;

namespace CRMCore.Module.Common.Controllers
{
    [Route("[controller]")]
    public class CommonController : Controller
    {
        public CommonController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
