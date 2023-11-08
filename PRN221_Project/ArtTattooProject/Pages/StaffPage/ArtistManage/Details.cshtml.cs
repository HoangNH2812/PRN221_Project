using System;
using System.Collections.Generic;
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
    public class DetailsModel : PageModel
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IConfiguration Configuration;
        private readonly IArtistRepository _artistRepository;
        private readonly ITattoosDesignRepository _tattoosDesignRepository;
        public DetailsModel(IArtistRepository artistRepository, IConfiguration configuration, IServiceRepository serviceRepository, ITattoosDesignRepository tattoosDesignRepository)
        {
            _artistRepository = artistRepository;
            _serviceRepository = serviceRepository;
            Configuration = configuration;
            _tattoosDesignRepository = tattoosDesignRepository;
        }

        public Artist Artist { get; set; } = default!;
        public IQueryable<Service> ServiceList { get; set; } = default!;
        public PaginatedList<Service> Service { get; set; } = default!;
        public IQueryable<TattoosDesign> TattoosDesignList { get; set; } = default!;
        public PaginatedList<TattoosDesign> TattoosDesign { get; set; } = default!;
        public IActionResult OnGet(int? id, int? pageIndexService, int? pageIndexTattoosDesign)
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
            int artistId = id.Value;
            Artist = _artistRepository.GetByID(artistId);

            ServiceList = _serviceRepository.GetByArtist(artistId).AsQueryable();
            var pageSize = Configuration.GetValue("PageSize", 4);
            Service = PaginatedList<Service>.Create(
                ServiceList, pageIndexService ?? 1, pageSize);

            TattoosDesignList = _tattoosDesignRepository.GetByArtist(artistId).AsQueryable();
            TattoosDesign = PaginatedList<TattoosDesign>.Create(
                TattoosDesignList, pageIndexTattoosDesign ?? 1, pageSize);
            return Page();
        }
    }
}
