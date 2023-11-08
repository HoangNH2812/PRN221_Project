using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.AdminPage.StyleManage
{
    public class EditModel : PageModel
    {
        private readonly IStyleRepository _styleRepository;
        public EditModel(IStyleRepository styleRepository)
        {
            _styleRepository = styleRepository;
        }

        [BindProperty]
        public Style Style { get; set; } = default!;

        public  IActionResult OnGet(int? id)
        {
            Account account = HttpContext.Session.GetObjectFromJson<Account>("account");
            if (account == null)
            {
                return RedirectToPage("/LoginPage");
            }
            else
            {
                string isAdmin = HttpContext.Session.GetString("isAdmin");
                if (isAdmin == null || isAdmin == "")
                {
                    return RedirectToPage("/LoginPage");
                }
                bool isADMIN = JsonConvert.DeserializeObject<Boolean>(isAdmin);
                if (!isADMIN)
                {
                    return RedirectToPage("/LoginPage");
                }
            }
            if (id == null)
            {
                return NotFound();
            }

            var style = _styleRepository.GetByID(id.Value);
            if (style == null)
            {
                return NotFound();
            }
            Style = style;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return OnGet(Style.StyleId);
            }

            try
            {
                _styleRepository.Update(Style);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StyleExists(Style.StyleId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool StyleExists(int id)
        {
          return (_styleRepository.GetByID(id)==null);
        }
    }
}
