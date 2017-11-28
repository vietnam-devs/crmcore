using CRMCore.Framework.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;

namespace CRMCore.Module.Common.Controllers
{
    [Area("CRMCore.Module.Common")]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public class CommonController : Controller
    {
        public IActionResult Index()
        {
            @ViewBag.Time = DateTime.Now.ToString();
            
            return View();
        }
    }

    public class TestEntity : BaseEntity
    {

    }
}
