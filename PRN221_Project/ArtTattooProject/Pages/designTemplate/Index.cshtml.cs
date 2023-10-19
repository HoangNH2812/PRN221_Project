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
    public class IndexModel : PageModel
    {
        private readonly Repositories.Models.ArtTattooLoverContext _context;

        public IndexModel(Repositories.Models.ArtTattooLoverContext context)
        {
            _context = context;
        }

        public IList<TattoosDesign> TattoosDesign { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.TattoosDesigns != null)
            {
                TattoosDesign = await _context.TattoosDesigns
                .Include(t => t.Artist)
                .Include(t => t.Style).ToListAsync();
            }
        }
    }
}
