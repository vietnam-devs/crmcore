using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
        private readonly IHostingEnvironment _env;

        public HomeController(IConfiguration config, IHostingEnvironment env)
        {
            _config = config;
            _env = env;
        }

        // fallback to SPA routes
        /*[Route("/{*url}")]
        [ResponseCache(NoStore = true)]
        public IActionResult Error(string url)
        {
            return RenderIndexFile();
        } */

        // This is for oclient.js processing
        [Route("/callback")]
        [ResponseCache(NoStore = true)]
        public IActionResult Callback()
        {
            return RenderIndexFile();
        }

        [ResponseCache(NoStore = true)]
        public IActionResult Index()
        {
            return RenderIndexFile();
        }

        private IActionResult RenderIndexFile()
        {
            var indexFilePath = Path.Combine("ClientApp/build/index.html");
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
