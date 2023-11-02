using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.ArtistPage.CertificateManage
{
    public class CreateModel : PageModel
    {
        private readonly ICertificateArtistRepository _certificateArtistRepository;
        private readonly ICertificateRepository _certificateRepository;
        public CreateModel(ICertificateArtistRepository certificateArtistRepository, ICertificateRepository certificateRepository = null)
        {
            _certificateArtistRepository = certificateArtistRepository;
            _certificateRepository = certificateRepository;
        }

        [BindProperty]
        public CertificateArtist CertificateArtist { get; set; } = default!;
        public string Msg { get; set; }
        public IActionResult OnGet()
        {
            ViewData["CertificateName"] = new SelectList(_certificateRepository.GetAll(), "CertificateId", "CertificateName");
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return OnGet();
            }
            try
            {
                CertificateArtist.ArtistId=HttpContext.Session.GetObjectFromJson<Account>("account").ArtistId;
                _certificateArtistRepository.AddNew(CertificateArtist);
            } catch (Exception ex)
            {
                Msg = ex.Message;
                return OnGet();
            }
            return RedirectToPage("./Index");
        }
    }
}
