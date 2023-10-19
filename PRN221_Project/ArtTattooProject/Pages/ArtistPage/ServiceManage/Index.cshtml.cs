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
        public IndexModel(IServiceRepository serviceRepository, IConfiguration configuration)
        {
            _serviceRepository = serviceRepository;
            Configuration = configuration;
        }

        public IQueryable<Service> ServiceList { get;set; } = default!;
        public PaginatedList<Service> Service { get;set; } = default!;

        public async Task OnGetAsync(int? pageIndex)
        {
            int artistId = HttpContext.Session.GetObjectFromJson<Account>("account").ArtistId.Value;
            ServiceList = _serviceRepository.GetByArtist(artistId).AsQueryable();
            var pageSize = Configuration.GetValue("PageSize", 4);
            Service = PaginatedList<Service>.Create(
                ServiceList, pageIndex ?? 1, pageSize);
        }
    }
}
