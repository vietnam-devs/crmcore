using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRMCore.ClientApp.Angular.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new VirtualFileResult("index.html", "text/html");
        }

        // GET: /<controller>/
        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
