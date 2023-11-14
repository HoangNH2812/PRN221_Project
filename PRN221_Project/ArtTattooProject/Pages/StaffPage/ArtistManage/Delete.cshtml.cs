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
using Repositories.Repository;

namespace ArtTattooProject.Pages.StaffPage.ArtistManage
{
    public class DeleteModel : PageModel
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IAccountRepository _accountRepository;
        public DeleteModel(IArtistRepository artistRepository, IAccountRepository accountRepository)
        {
            _artistRepository = artistRepository;
            _accountRepository = accountRepository;
        }

        [BindProperty]
        public Artist Artist { get; set; } = default!;
        public Account AccountArtist { get; set; } = default!;

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
            var accountArtist = _accountRepository.GetById(artist.ArtistId, null, null);
            if (artist == null && account != null)
            {
                return NotFound();
            }
            else
            {
                Artist = artist;
                AccountArtist = accountArtist;
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
                var account = _accountRepository.GetById(artist.ArtistId, null, null);
                if (account != null)
                {
                    Account Account = account;
                    if (Account.Status == 1) account.Status = 0; else account.Status = 1;
                    _accountRepository.Update(Account);
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
