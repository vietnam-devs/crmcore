using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace CRMCore.Module.Spa.Controllers
{
    // [Authorize]
    [Area("CRMCore.Module.Spa")]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;
        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        [Route("/callback")]
        public IActionResult Callback()
        {
            return RenderIndexFile();
        }

        public IActionResult Index()
        {
            return RenderIndexFile();
        }

        private IActionResult RenderIndexFile()
        {
            var indexFilePath = Path.Combine("wwwroot/index.html");
            var htmlContent = System.IO.File.ReadAllText(indexFilePath);

            var scriptIndex = htmlContent.IndexOf("<script", StringComparison.OrdinalIgnoreCase);
            if (scriptIndex < 0)
            {
                return Content(htmlContent, new MediaTypeHeaderValue("text/html").ToString());
            }

            var spaSection = _config.GetSection("SPA");

            var jsObject = spaSection.GetChildren()
                .Aggregate(
                    new StringBuilder(),
                    (builder, item) => builder.Append($"{item.Key}:'{item.Value}',"));

            var updatedHtmlContent = htmlContent.Insert(scriptIndex, $"<script>window.serverVariables = {{{jsObject}}};</script>");

            return Content(updatedHtmlContent, new MediaTypeHeaderValue("text/html").ToString());
        }
    }
}
