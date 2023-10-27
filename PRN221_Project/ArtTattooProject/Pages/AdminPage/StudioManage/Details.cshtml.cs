using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
