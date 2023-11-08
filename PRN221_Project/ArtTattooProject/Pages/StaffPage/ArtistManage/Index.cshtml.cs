using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.StaffPage.ArtistManage
{
    public class IndexModel : PageModel
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IConfiguration Configuration;
        public IndexModel(IArtistRepository artistRepository, IStaffRepository staffRepository, IConfiguration configuration)
        {
            _artistRepository = artistRepository;
            _staffRepository = staffRepository;
            Configuration = configuration;
        }

        public IQueryable<Artist> ArtistList { get;set; } = default!;
        public PaginatedList<Artist> Artist { get;set; } = default!;

        public IActionResult OnGet(int? pageIndex)
        {
            Account account = HttpContext.Session.GetObjectFromJson<Account>("account");
            if (account == null)
            {
                return RedirectToPage("/LoginPage");
            }
            else if (account.StaffId == null)
            {
                return RedirectToPage("/LoginPage");
            }
            int StaffId = HttpContext.Session.GetObjectFromJson<Account>("account").StaffId.Value;
            Staff staff = _staffRepository.GetByID(StaffId);
            ArtistList = _artistRepository.GetByStudio(staff.StudioId.Value).AsQueryable();

            var pageSize = Configuration.GetValue("PageSize", 4);
            Artist = PaginatedList<Artist>.Create(
                ArtistList, pageIndex ?? 1, pageSize);

            return Page();
        }
    }
}
