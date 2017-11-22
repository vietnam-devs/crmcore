using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMCore.Module.Data;
using CRMCore.Module.Identity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRMCore.Module.Identity.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public LoginViewModel LoginVM { get; set; }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

             _db.Database.EnsureCreated();

            return RedirectToPage("/Index");
        }
    }
}
