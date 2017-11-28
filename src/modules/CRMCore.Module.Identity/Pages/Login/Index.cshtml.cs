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
using IdentityServer4.Models;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace CRMCore.Module.Identity.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly ILoginService<ApplicationUser> _loginService;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(ILoginService<ApplicationUser> loginService,
                          SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction, 
            IClientStore clientStore,
            ILogger<IndexModel> logger,
            UserManager<ApplicationUser> userManager)
        {
             _loginService = loginService;
             _interaction = interaction;
             _clientStore = clientStore;
             _logger = logger;
             _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public LoginViewModel LoginVM { get; set; }

        public async Task<IActionResult> OnGetAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null)
            {
                // if IdP is passed, then bypass showing the login screen
                return OnGetExternalLogin(context.IdP, returnUrl);
            }

            LoginVM = await BuildLoginViewModelAsync(returnUrl, context);
            ViewData["ReturnUrl"] = returnUrl;

            return Page();
        }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _loginService.FindByUsername(LoginVM.UserName);
                          
                if (await _loginService.ValidateCredentials(user, LoginVM.Password))
                {
                    AuthenticationProperties props = null;
                    if (LoginVM.RememberMe)
                    {
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.AddYears(1)
                        };
                    };

                    await _loginService.SignIn(user);

                    // make sure the returnUrl is still valid, and if yes - redirect back to authorize endpoint
                    //if (_interaction.IsValidReturnUrl(LoginVM.ReturnUrl))
                    //{
                        return Redirect(LoginVM.ReturnUrl);
                   // }

                    //return Redirect("~/");
                }

                ModelState.AddModelError("", "Invalid username or password.");
            }

            // something went wrong, show form with error
            var vm = await BuildLoginViewModelAsync(LoginVM);

            ViewData["ReturnUrl"] = LoginVM.ReturnUrl;
            return Page();
        }

        public IActionResult OnGetExternalLogin(string provider, string returnUrl)
        {
            if (returnUrl != null)
            {
                returnUrl = UrlEncoder.Default.Encode(returnUrl);
            }
            returnUrl = "/identity/externallogincallback?returnUrl=" + returnUrl;

            // start challenge and roundtrip the return URL
            var props = new AuthenticationProperties
            {
                RedirectUri = returnUrl,
                Items = { { "scheme", provider } }
            };
            return new ChallengeResult(provider, props);
        }

        async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl, AuthorizationRequest context)
        {
            var allowLocal = true;
            if (context?.ClientId != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(context.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;
                }
            }

            return new LoginViewModel
            {
                ReturnUrl = returnUrl,
                UserName = context?.LoginHint,
            };
        }

        async Task<LoginViewModel> BuildLoginViewModelAsync(LoginViewModel model)
        {
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl, context);
            vm.UserName = model.UserName;
            vm.RememberMe = model.RememberMe;
            return vm;
        }
    }
}
