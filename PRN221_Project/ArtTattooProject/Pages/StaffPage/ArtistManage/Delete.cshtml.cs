using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.StaffPage.ArtistManage
{
    public class DeleteModel : PageModel
    {
        private readonly IArtistRepository _artistRepository;
        public DeleteModel(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        [BindProperty]
      public Artist Artist { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Account account = HttpContext.Session.GetObjectFromJson<Account>("account");
            if (account == null)
            {
                return RedirectToPage("/LoginPage");
            }
            else if (account.StaffId == null)
            {
                return RedirectToPage("/LoginPage");
            }
            if (id == null)
            {
                return NotFound();
            }

            var artist = _artistRepository.GetByID(id.Value);

            if (artist == null)
            {
                return NotFound();
            }
            else 
            {
                Artist = artist;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var artist = _artistRepository.GetByID(id.Value);

            if (artist != null)
            {
                Artist = artist;
                _artistRepository.Delete(Artist);
            }

            return RedirectToPage("./Index");
        }
    }
}
