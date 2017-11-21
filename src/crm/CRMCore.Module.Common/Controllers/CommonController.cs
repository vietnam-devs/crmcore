using Microsoft.AspNetCore.Mvc;
using System;

namespace CRMCore.Module.Common.Controllers
{
    [Area("CRMCore.Module.Common")]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CommonController : Controller
    {
        public CommonController()
        {
        }

        public IActionResult Index()
        {
            @ViewBag.Time = DateTime.Now.ToString();
            return View();
        }
    }
}
