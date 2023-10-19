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

namespace ArtTattooProject.Pages.ArtistPage.DesignManage
{
    public class IndexModel : PageModel
    {
        private readonly ITattoosDesignRepository _tattoosDesignRepository;
        private readonly IConfiguration Configuration;
        public IndexModel(ITattoosDesignRepository tattoosDesignRepository, IConfiguration configuration)
        {
            _tattoosDesignRepository = tattoosDesignRepository;
            Configuration = configuration;
        }

        public IQueryable<TattoosDesign> TattoosDesignList { get;set; } = default!;
        public PaginatedList<TattoosDesign> TattoosDesign { get;set; } = default!;

        public async Task OnGetAsync(int? pageIndex)
        {
            int artistId = HttpContext.Session.GetObjectFromJson<Account>("account").ArtistId.Value;
            TattoosDesignList = _tattoosDesignRepository.GetByArtist(artistId).AsQueryable();
            
            var pageSize = Configuration.GetValue("PageSize", 4);
            TattoosDesign = PaginatedList<TattoosDesign>.Create(
                TattoosDesignList, pageIndex ?? 1, pageSize);
        }
    }
}
