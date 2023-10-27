using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;

namespace ArtTattooProject.Pages.AdminPage.StudioManage
{
    public class DeleteModel : PageModel
    {
        private readonly Repositories.Models.ArtTattooLoverContext _context;

        public DeleteModel(Repositories.Models.ArtTattooLoverContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Studio Studio { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Studios == null)
            {
                return NotFound();
            }

            var studio = await _context.Studios.FirstOrDefaultAsync(m => m.StudioId == id);

            if (studio == null)
            {
                return NotFound();
            }
            else 
            {
                Studio = studio;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Studios == null)
            {
                return NotFound();
            }
            var studio = await _context.Studios.FindAsync(id);

            if (studio != null)
            {
                Studio = studio;
                _context.Studios.Remove(Studio);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
