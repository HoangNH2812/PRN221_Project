using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using Repositories.Models;
using Repositories.Repository;

namespace ArtTattooProject.Pages.ArtistPage.CertificateManage
{
    public class EditModel : PageModel
    {

        private readonly ICertificateArtistRepository _certificateArtistRepository;
        public EditModel(ICertificateArtistRepository certificateArtistRepository)
        {
            _certificateArtistRepository = certificateArtistRepository;
        }
        [BindProperty]
        public CertificateArtist CertificateArtist { get; set; }

        public IActionResult OnGet(int? certId, int? artistId)
        {
            if (certId != null && artistId != null)
            {
                CertificateArtist = _certificateArtistRepository.GetCertificateArtist(certId.Value, artistId.Value);
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                CertificateArtist oldCertificateArtist = _certificateArtistRepository.GetCertificateArtist(CertificateArtist.CertificateId.Value,CertificateArtist.ArtistId.Value);
                if (oldCertificateArtist.Urllink != CertificateArtist.Urllink) {
                    _certificateArtistRepository.Update(CertificateArtist);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}
