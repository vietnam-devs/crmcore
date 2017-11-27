using System.Threading.Tasks;
using CRMCore.Module.Identity.Services;
using CRMCore.Module.Identity.ViewModels;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using CRMCore.Framework.Entities.Identity;

namespace CRMCore.Module.Identity.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly ILoginService<ApplicationUser> _loginService;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(/*ILoginService<ApplicationUser> loginService,
            IIdentityServerInteractionService interaction, 
            IClientStore clientStore,
            ILogger<IndexModel> logger,
            UserManager<ApplicationUser> userManager*/)
        {
            // _loginService = loginService;
            // _interaction = interaction;
            // _clientStore = clientStore;
            // _logger = logger;
            // _userManager = userManager;
        }

        [BindProperty]
        public LoginViewModel LoginVM { get; set; }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _loginService.FindByUsername(LoginVM.UserName);
            if (await _loginService.ValidateCredentials(user, LoginVM.Password))
            {
            }

            return RedirectToPage("/Index");
        }
    }
}
