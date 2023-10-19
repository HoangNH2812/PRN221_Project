using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;

namespace ArtTattooProject.Pages.designTemplate
{
    public class EditModel : PageModel
    {
        private readonly Repositories.Models.ArtTattooLoverContext _context;

        public EditModel(Repositories.Models.ArtTattooLoverContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TattoosDesign TattoosDesign { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TattoosDesigns == null)
            {
                return NotFound();
            }

            var tattoosdesign =  await _context.TattoosDesigns.FirstOrDefaultAsync(m => m.TattoosDesignId == id);
            if (tattoosdesign == null)
            {
                return NotFound();
            }
            TattoosDesign = tattoosdesign;
           ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "ArtistId");
           ViewData["StyleId"] = new SelectList(_context.Styles, "StyleId", "StyleId");
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

            _context.Attach(TattoosDesign).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TattoosDesignExists(TattoosDesign.TattoosDesignId))
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

        private bool TattoosDesignExists(int id)
        {
          return (_context.TattoosDesigns?.Any(e => e.TattoosDesignId == id)).GetValueOrDefault();
        }
    }
}
