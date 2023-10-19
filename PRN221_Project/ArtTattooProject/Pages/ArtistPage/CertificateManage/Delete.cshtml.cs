using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.ArtistPage.CertificateManage
{
    public class DeleteModel : PageModel
    {
        private readonly ICertificateArtistRepository _certificateArtistRepository;
        public DeleteModel(ICertificateArtistRepository certificateArtistRepository) {
            _certificateArtistRepository = certificateArtistRepository;
        }

        [BindProperty]
        public CertificateArtist CertificateArtist { get; set; } = default!;
        public string Msg { get; set; }
        public IActionResult OnGet(int? CertificateId,int? ArtistId)
        {
          
            if (CertificateId != null && ArtistId != null) {
                var certificateArtist = _certificateArtistRepository.GetCertificateArtist(CertificateId.Value, ArtistId.Value);
                CertificateArtist = certificateArtist;
            } else
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(int? CertificateId, int? ArtistId)
        {
            if (CertificateId != null && ArtistId != null)
            {
                var certificateArtist = _certificateArtistRepository.GetCertificateArtist(CertificateId.Value, ArtistId.Value);
                CertificateArtist = certificateArtist;
            }
            try
            {
                _certificateArtistRepository.Delete(CertificateArtist);
            } catch (Exception ex)
            {
                Msg = ex.Message;
            }
            return RedirectToPage("./Index");
        }
    }
}
