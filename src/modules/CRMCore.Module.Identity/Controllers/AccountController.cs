using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRMCore.Module.Identity.Controllers
{
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
