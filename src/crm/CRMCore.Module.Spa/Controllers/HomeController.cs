using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace CRMCore.Module.Spa.Controllers
{
    [Area("CRMCore.Module.Spa")]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var _htmlContent = System.IO.File.ReadAllText("wwwroot\\index.html");
            return Content(_htmlContent, new MediaTypeHeaderValue("text/html").ToString());
        }
    }
}
