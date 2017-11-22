using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMCore.Module.Identity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRMCore.Module.Identity.Pages.Login
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public LoginViewModel LoginVM { get; set; }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            return RedirectToPage("/Index");
        }
    }
}
