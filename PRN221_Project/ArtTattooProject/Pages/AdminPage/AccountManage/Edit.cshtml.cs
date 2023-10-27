using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;

namespace ArtTattooProject.Pages.AdminPage.AccountManage
{
    public class EditModel : PageModel
    {
        private readonly Repositories.Models.ArtTattooLoverContext _context;

        public EditModel(Repositories.Models.ArtTattooLoverContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Account Account { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account =  await _context.Accounts.FirstOrDefaultAsync(m => m.Username == id);
            if (account == null)
            {
                return NotFound();
            }
            Account = account;
           ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "ArtistId");
           ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId");
           ViewData["TattooLoverId"] = new SelectList(_context.TattooLovers, "TattooLoverId", "TattooLoverId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(Account.Username))
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

        private bool AccountExists(string id)
        {
          return (_context.Accounts?.Any(e => e.Username == id)).GetValueOrDefault();
        }
    }
}
