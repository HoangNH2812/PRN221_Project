using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.AdminPage.StudioManage
{
    public class DetailsModel : PageModel
    {
        private readonly IStudioRepository _studioRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IConfiguration Configuration;

        public DetailsModel(IStudioRepository studioRepository, IArtistRepository artistRepository, IConfiguration configuration, IStaffRepository staffRepository)
        {
            _studioRepository = studioRepository;
            _artistRepository = artistRepository;
            Configuration = configuration;
            _staffRepository = staffRepository;
        }

        public Studio Studio { get; set; } = default!; 
      public IQueryable<Artist> ArtistList { get; set; } = default!;
      public PaginatedList<Artist> Artist { get; set; } = default!;
        public IQueryable<Staff> StaffList { get; set; } = default!;
        public PaginatedList<Staff> Staff { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id, int? pageIndexArtist, int? pageIndexStaff)
        {
            Account account = HttpContext.Session.GetObjectFromJson<Account>("account");
            if (account == null)
            {
                return RedirectToPage("/LoginPage");
            }
            else
            {
                string isAdmin = HttpContext.Session.GetString("isAdmin");
                if (isAdmin == null || isAdmin == "")
                {
                    return RedirectToPage("/LoginPage");
                }
                bool isADMIN = JsonConvert.DeserializeObject<Boolean>(isAdmin);
                if (!isADMIN)
                {
                    return RedirectToPage("/LoginPage");
                }
            }
            if (id == null)
            {
                return NotFound();
            }

            var studio = _studioRepository.GetByID(id.Value);
            if (studio == null)
            {
                return NotFound();
            }
            else 
            {
                Studio = studio;
                ArtistList = _artistRepository.GetByStudio(Studio.StudioId).AsQueryable();
                StaffList = _staffRepository.GetByStudioId(Studio.StudioId).AsQueryable();

                var pageSize = Configuration.GetValue("PageSize", 4);

                Staff = PaginatedList<Staff>.Create(
                    StaffList, pageIndexStaff ?? 1, pageSize);
                Artist = PaginatedList<Artist>.Create(
                    ArtistList, pageIndexArtist ?? 1, pageSize);
            }
            return Page();
        }
    }
}
