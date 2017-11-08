using System;
using Microsoft.AspNetCore.Mvc;

namespace CRMCore.Module.Common.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
