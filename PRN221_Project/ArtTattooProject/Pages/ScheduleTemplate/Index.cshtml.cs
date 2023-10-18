﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;

namespace ArtTattooProject.Pages.ScheduleTemplate
{
    public class IndexModel : PageModel
    {
        private readonly Repositories.Models.ArtTattooLoverContext _context;

        public IndexModel(Repositories.Models.ArtTattooLoverContext context)
        {
            _context = context;
        }

        public IList<Schedule> Schedule { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Schedules != null)
            {
                Schedule = await _context.Schedules
                .Include(s => s.Artist).ToListAsync();
            }
        }
    }
}
