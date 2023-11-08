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

namespace ArtTattooProject.Pages.AdminPage.CertificateManage
{
    public class IndexModel : PageModel
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly IConfiguration Configuration;
        public IndexModel(ICertificateRepository certificateRepository, IConfiguration configuration)
        {
            _certificateRepository = certificateRepository;
            Configuration = configuration;
        }

        public IQueryable<Certificate> CertificateList { get;set; } = default!;
        public PaginatedList<Certificate> Certificate { get;set; } = default!;

        public  IActionResult OnGet(int? pageIndex)
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
            CertificateList = _certificateRepository.GetAll().AsQueryable();
            var pageSize = Configuration.GetValue("PageSize", 4);

            Certificate = PaginatedList<Certificate>.Create(
                CertificateList, pageIndex ?? 1, pageSize);
            return Page();
        }
    }
}
