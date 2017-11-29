using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMCore.Module.Identity.ViewModels;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CRMCore.Module.Identity.Pages.Consent
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IClientStore _clientStore;
        private readonly IResourceStore _resourceStore;
        private readonly IIdentityServerInteractionService _interaction;


        public IndexModel(
            ILogger<IndexModel> logger,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IResourceStore resourceStore)
        {
            _logger = logger;
            _interaction = interaction;
            _clientStore = clientStore;
            _resourceStore = resourceStore;
        }

        [BindProperty]
        public ConsentViewModel ConsentVM { get; set; }

        public async Task<IActionResult> OnGet(string returnUrl)
        {
            ConsentVM = await BuildViewModelAsync(returnUrl);

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var request = await _interaction.GetAuthorizationContextAsync(ConsentVM.ReturnUrl);
            ConsentResponse response = null;

            // user clicked 'no' - send back the standard 'access_denied' response
            if (ConsentVM.Button == "no")
            {
                response = ConsentResponse.Denied;
            }
            // user clicked 'yes' - validate the data
            else if (ConsentVM.Button == "yes" && ConsentVM != null)
            {
                // if the user consented to some scope, build the response model
                if (ConsentVM.ScopesConsented != null && ConsentVM.ScopesConsented.Any())
                {
                    response = new ConsentResponse
                    {
                        RememberConsent = ConsentVM.RememberConsent,
                        ScopesConsented = ConsentVM.ScopesConsented
                    };
                }
                else
                {
                    ModelState.AddModelError("", "You must pick at least one permission.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid Selection");
            }

            if (response != null)
            {
                // communicate outcome of consent back to identityserver
                await _interaction.GrantConsentAsync(request, response);

                // redirect back to authorization endpoint
                return Redirect(ConsentVM.ReturnUrl);
            }

            ConsentVM = await BuildViewModelAsync(ConsentVM.ReturnUrl, ConsentVM);

            return Page();
        }

        async Task<ConsentViewModel> BuildViewModelAsync(string returnUrl, ConsentInputModel model = null)
        {
            var request = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (request != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(request.ClientId);
                if (client != null)
                {
                    var resources = await _resourceStore.FindEnabledResourcesByScopeAsync(request.ScopesRequested);
                    if (resources != null && (resources.IdentityResources.Any() || resources.ApiResources.Any()))
                    {
                        return new ConsentViewModel(model, returnUrl, request, client, resources);
                    }
                    else
                    {
                        _logger.LogError("No scopes matching: {0}", request.ScopesRequested.Aggregate((x, y) => x + ", " + y));
                    }
                }
                else
                {
                    _logger.LogError("Invalid client id: {0}", request.ClientId);
                }
            }
            else
            {
                _logger.LogError("No consent request matching request: {0}", returnUrl);
            }

            return null;
        }
    }
}
