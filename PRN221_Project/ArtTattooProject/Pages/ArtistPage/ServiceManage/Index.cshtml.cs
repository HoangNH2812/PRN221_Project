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
using Repositories.Repository;

namespace ArtTattooProject.Pages.ArtistPage.ServiceManage
{
    public class IndexModel : PageModel
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IConfiguration Configuration;
        private readonly ITattoosDesignRepository _taxtoosDesignRepository;
        public IndexModel(IServiceRepository serviceRepository, IConfiguration configuration, ITattoosDesignRepository taxtoosDesignRepository)
        {
            _serviceRepository = serviceRepository;
            Configuration = configuration;
            _taxtoosDesignRepository = taxtoosDesignRepository;
        }

        public IQueryable<Service> ServiceList { get;set; } = default!;
        public PaginatedList<Service> Service { get;set; } = default!;

        public async Task OnGetAsync(int? pageIndex)
        {
            int artistId = HttpContext.Session.GetObjectFromJson<Account>("account").ArtistId.Value;
            IEnumerable<Service> list = _serviceRepository.GetByArtist(artistId);
            foreach (var item in list)
            {
                if (item.TattoosDesignId != null)
                {
                    item.TattoosDesign = _taxtoosDesignRepository.GetByID(item.TattoosDesignId.Value);
                }
            }
            ServiceList = list.AsQueryable();
            var pageSize = Configuration.GetValue("PageSize", 4);
            Service = PaginatedList<Service>.Create(
                ServiceList, pageIndex ?? 1, pageSize);
        }
    }
}
