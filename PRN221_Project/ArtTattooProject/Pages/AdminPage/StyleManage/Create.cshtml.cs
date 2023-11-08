using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.AdminPage.StyleManage
{
    public class CreateModel : PageModel
    {
        private readonly IStyleRepository _styleRepository;

        public CreateModel(IStyleRepository styleRepository)
        {
            _styleRepository = styleRepository;
        }

        public IActionResult OnGet()
        {
            Account account = HttpContext.Session.GetObjectFromJson<Account>("account");
            if (account == null)
            {
                return RedirectToPage("../LoginPage");
            }
            else
            {
                string isAdmin = HttpContext.Session.GetString("isAdmin");
                if (isAdmin == null || isAdmin == "")
                {
                    return RedirectToPage("../LoginPage");
                }
                bool isADMIN = JsonConvert.DeserializeObject<Boolean>(isAdmin);
                if (!isADMIN)
                {
                    return RedirectToPage("../LoginPage");
                }
            }
            return Page();
        }

        [BindProperty]
        public Style Style { get; set; } = default!;
        public string Msg { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _styleRepository.AddNew(Style);
            }catch (Exception ex)
            {
                Msg = ex.Message;
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
