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
    public class IndexModel : PageModel
    {
        private readonly IStudioRepository _studioRepository;
        private readonly IConfiguration Configuration;

        public IndexModel(IStudioRepository studioRepository, IConfiguration configuration)
        {
            _studioRepository = studioRepository;
            Configuration = configuration;
        }

        public IQueryable<Studio> StudioList { get;set; } = default!;
        public PaginatedList<Studio> Studio { get;set; } = default!;

        public async Task OnGetAsync(int? pageIndex)
        {
            StudioList = _studioRepository.GetAll().AsQueryable();
            var pageSize = Configuration.GetValue("PageSize", 4);
            Studio = PaginatedList<Studio>.Create(
                StudioList, pageIndex ?? 1, pageSize);
        }
    }
}
