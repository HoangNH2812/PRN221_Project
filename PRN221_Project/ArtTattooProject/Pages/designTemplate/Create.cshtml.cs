using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories.Models;

namespace ArtTattooProject.Pages.designTemplate
{
    public class CreateModel : PageModel
    {
        private readonly Repositories.Models.ArtTattooLoverContext _context;

        public CreateModel(Repositories.Models.ArtTattooLoverContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "ArtistId");
        ViewData["StyleId"] = new SelectList(_context.Styles, "StyleId", "StyleId");
            return Page();
        }

        [BindProperty]
        public TattoosDesign TattoosDesign { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.TattoosDesigns == null || TattoosDesign == null)
            {
                return Page();
            }

            _context.TattoosDesigns.Add(TattoosDesign);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
