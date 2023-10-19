using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;

namespace ArtTattooProject.Pages.designTemplate
{
    public class DetailsModel : PageModel
    {
        private readonly Repositories.Models.ArtTattooLoverContext _context;

        public DetailsModel(Repositories.Models.ArtTattooLoverContext context)
        {
            _context = context;
        }

      public TattoosDesign TattoosDesign { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TattoosDesigns == null)
            {
                return NotFound();
            }

            var tattoosdesign = await _context.TattoosDesigns.FirstOrDefaultAsync(m => m.TattoosDesignId == id);
            if (tattoosdesign == null)
            {
                return NotFound();
            }
            else 
            {
                TattoosDesign = tattoosdesign;
            }
            return Page();
        }
    }
}
