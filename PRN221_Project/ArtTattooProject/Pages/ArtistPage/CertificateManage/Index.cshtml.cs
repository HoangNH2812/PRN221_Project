using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.ArtistPage.CertificateManage
{
    public class IndexModel : PageModel
    {
        private readonly ICertificateArtistRepository _certificateArtistRepository;
        private readonly IConfiguration Configuration;
        public IndexModel(ICertificateArtistRepository certificateArtistRepository, IConfiguration configuration)
        {
            _certificateArtistRepository = certificateArtistRepository;
            Configuration = configuration;
        }

        public IQueryable<CertificateArtist> CertificateArtistList { get; set; } = default!;
        public PaginatedList<CertificateArtist> CertificateArtist { get; set; } = default!;
        public IActionResult OnGet(int? pageIndex)
        {
            Account account = HttpContext.Session.GetObjectFromJson<Account>("account");
            if (account == null)
            {
                return RedirectToPage("/LoginPage");
            }
            else if (account.ArtistId == null)
            {
                return RedirectToPage("/LoginPage");
            }
            int artistId = HttpContext.Session.GetObjectFromJson<Account>("account").ArtistId.Value;
            CertificateArtistList = _certificateArtistRepository.GetByArtistID(artistId).AsQueryable();

            var pageSize = Configuration.GetValue("PageSize", 4);
            CertificateArtist = PaginatedList<CertificateArtist>.Create(
                CertificateArtistList, pageIndex ?? 1, pageSize);

            return Page();
        }
    }
}
