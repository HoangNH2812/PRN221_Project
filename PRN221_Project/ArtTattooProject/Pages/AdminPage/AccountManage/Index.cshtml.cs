using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;

namespace ArtTattooProject.Pages.AdminPage.AccountManage
{
    public class IndexModel : PageModel
    {
        private readonly Repositories.Models.ArtTattooLoverContext _context;

        public IndexModel(Repositories.Models.ArtTattooLoverContext context)
        {
            _context = context;
        }

        public IList<Account> Account { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Accounts != null)
            {
                Account = await _context.Accounts
                .Include(a => a.Artist)
                .Include(a => a.Staff)
                .Include(a => a.TattooLover).ToListAsync();
            }
        }
    }
}
