using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;

namespace ArtTattooProject.Pages.CRUDTemplate
{
    public class IndexModel : PageModel
    {
        private readonly Repositories.Models.ArtTattooLoverContext _context;

        public IndexModel(Repositories.Models.ArtTattooLoverContext context)
        {
            _context = context;
        }

        public IList<Appointment> Appointment { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Appointments != null)
            {
                Appointment = await _context.Appointments
                .Include(a => a.Studio)
                .Include(a => a.TattooLover).ToListAsync();
            }
        }
    }
}
