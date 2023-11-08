using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Text;
using System.Linq;
using System.Threading.Tasks;
using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.AdminPage.StyleManage
{
    public class IndexModel : PageModel
    {
        private readonly IStyleRepository _styleRepository;
        private readonly IConfiguration Configuration;
        private readonly ITattoosDesignRepository _taxtoosDesignRepository;
        public IndexModel(ITattoosDesignRepository taxtoosDesignRepository, IStyleRepository styleRepository, IConfiguration configuration)
        {
            _taxtoosDesignRepository = taxtoosDesignRepository;
            _styleRepository = styleRepository;
            Configuration = configuration;
        }

        public IQueryable<StyleMapper> StyleList { get;set; } = default!;
        public PaginatedList<StyleMapper> Style { get;set; } = default!;

        public  IActionResult OnGet(int? pageIndex)
        {
            Account account = HttpContext.Session.GetObjectFromJson<Account>("account");
            if (account == null)
            {
                return RedirectToPage("../LoginPage");
            }
            else
            {
                string isAdmin = HttpContext.Session.GetString("isAdmin");
                if (isAdmin == null || isAdmin == "")
                {
                    return RedirectToPage("../LoginPage");
                }
                bool isADMIN = JsonConvert.DeserializeObject<Boolean>(isAdmin);
                if (!isADMIN)
                {
                    return RedirectToPage("../LoginPage");
                }
            }
            IEnumerable<Style> styleList = _styleRepository.GetAll();
            List<StyleMapper> styleMapper = new List<StyleMapper>();
            foreach (Style style in styleList)
            {
                styleMapper.Add(new StyleMapper(style,_taxtoosDesignRepository.CountByStyle(style.StyleId)));
            }

            StyleList = styleMapper.AsQueryable();
            var pageSize = Configuration.GetValue("PageSize", 4);
            Style = PaginatedList<StyleMapper>.Create(
                StyleList, pageIndex ?? 1, pageSize);

            return Page();
        }
    }

    public class StyleMapper
    {
        public Style Style { get; set; }
        public int Count { get; set; }
        public StyleMapper(Style style, int count) {
            Style = style;
            Count = count;
        }
    }
}

