using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.AdminPage.CertificateManage
{
    public class CreateModel : PageModel
    {
        private readonly ICertificateRepository _certificateRepository;
        public CreateModel(ICertificateRepository certificateRepository)
        {
            _certificateRepository = certificateRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Certificate Certificate { get; set; } = default!;

        public string? Msg { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _certificateRepository.AddNew(Certificate);
            } catch (Exception ex)
            {
                Msg = ex.Message;
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
