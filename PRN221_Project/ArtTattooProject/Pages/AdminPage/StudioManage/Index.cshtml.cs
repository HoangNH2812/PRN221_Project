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

        public IActionResult OnGetAsync(int? pageIndex)
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
            StudioList = _studioRepository.GetAll().AsQueryable();
            var pageSize = Configuration.GetValue("PageSize", 4);
            Studio = PaginatedList<Studio>.Create(
                StudioList, pageIndex ?? 1, pageSize);

            return Page();
        }
    }
}
