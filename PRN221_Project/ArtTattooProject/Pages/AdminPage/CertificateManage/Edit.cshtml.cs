using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.AdminPage.CertificateManage
{
    public class EditModel : PageModel
    {
        private readonly ICertificateRepository _certificateRepository;

        public EditModel(ICertificateRepository certificateRepository)
        {
            _certificateRepository = certificateRepository;
        }

        [BindProperty]
        public Certificate Certificate { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
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
            if (id == null )
            {
                return NotFound();
            }

            var certificate = _certificateRepository.GetByID(id.Value);
            if (certificate == null)
            {
                return NotFound();
            }
            Certificate = certificate;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                Certificate cer = _certificateRepository.GetByID(Certificate.CertificateId);
                if (cer == null)
                {
                    return NotFound();
                } else
                {
                    _certificateRepository.Update(Certificate);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CertificateExists(Certificate.CertificateId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CertificateExists(int id)
        {
          return (_certificateRepository.GetByID(id)==null);
        }
    }
}
