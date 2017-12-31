using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Threading.Tasks;

namespace CRMCore.Module.Identity.Services
{
    public class CustomRedirectUriValidator : IRedirectUriValidator
    {
        public Task<bool> IsPostLogoutRedirectUriValidAsync(string requestedUri, Client client)
        {
            var result = client.PostLogoutRedirectUris.Contains(requestedUri);
            return Task.FromResult(result);
        }

        public Task<bool> IsRedirectUriValidAsync(string requestedUri, Client client)
         {
            var result = false;
            foreach (var redirectUri in client.RedirectUris)
            {
                if (requestedUri.StartsWith(redirectUri, StringComparison.InvariantCultureIgnoreCase))
                {
                    result = true;
                    break;
                }
            }
            return Task.FromResult(result);
        }
    }
}
