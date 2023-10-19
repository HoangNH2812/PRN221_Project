using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories.Models;

namespace ArtTattooProject.Pages.CRUDTemplate
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
        ViewData["StudioId"] = new SelectList(_context.Studios, "StudioId", "StudioId");
        ViewData["TattooLoverId"] = new SelectList(_context.TattooLovers, "TattooLoverId", "TattooLoverId");
            return Page();
        }

        [BindProperty]
        public Appointment Appointment { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Appointments == null || Appointment == null)
            {
                return Page();
            }

            _context.Appointments.Add(Appointment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
