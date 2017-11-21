using Microsoft.AspNetCore.Mvc;

namespace CRMCore.Module.Identity.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AccountController : Controller
    {
        //private readonly UserManager<IdentityUser> _userManager;

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
