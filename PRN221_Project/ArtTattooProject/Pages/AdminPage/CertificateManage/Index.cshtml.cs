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

        public  void OnGet(int? pageIndex)
        {
            CertificateList = _certificateRepository.GetAll().AsQueryable();
            var pageSize = Configuration.GetValue("PageSize", 4);

            Certificate = PaginatedList<Certificate>.Create(
                CertificateList, pageIndex ?? 1, pageSize);
        }
    }
}
