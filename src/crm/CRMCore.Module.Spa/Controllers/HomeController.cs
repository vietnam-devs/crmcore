using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http.Headers;

namespace CRMCore.Module.Spa.Controllers
{
    [Area("CRMCore.Module.Spa")]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var indexFilePath = Path.Combine("wwwroot/index.html");
            var _htmlContent = System.IO.File.ReadAllText(indexFilePath);
            return Content(_htmlContent, new MediaTypeHeaderValue("text/html").ToString());
        }
    }
}
