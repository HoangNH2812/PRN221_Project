using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.StaffPage.ArtistManage
{
    public class CreateModel : PageModel
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IStudioRepository _studioRepository;
        private readonly IStaffRepository _staffRepository;

        public CreateModel(IAccountRepository accountRepository, IArtistRepository artistRepository, IStudioRepository studioRepository, IStaffRepository staffRepository)
        {
            _accountRepository = accountRepository;
            _artistRepository = artistRepository;
            _studioRepository = studioRepository;
            _staffRepository = staffRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        public string Msg { get; set; }
        public Studio studio { get; set; }
        [BindProperty]
        public Artist Artist { get; set; } = default!;

        [BindProperty]
        public Account Account { get; set; } = default!;
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                int staffid = HttpContext.Session.GetObjectFromJson<Account>("account").StaffId.Value;
                Staff staff = _staffRepository.GetByID(staffid);
                studio = _studioRepository.GetByID(staff.StudioId.Value);
                return Page();
            }

            try
            {
                int staffid = HttpContext.Session.GetObjectFromJson<Account>("account").StaffId.Value;
                Staff staff = _staffRepository.GetByID(staffid);

                Artist.StudioId = staff.StudioId.Value;
                int artistId = _artistRepository.AddNew(Artist);
                try
                {
                    Account.ArtistId = artistId;
                    _accountRepository.AddNew(Account);
                }
                catch (Exception ex)
                {
                    Msg = ex.Message;
                    _artistRepository.Delete(Artist);
                    return Page();
                }

            }
            catch (Exception ex)
            {
                Msg = ex.Message;
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
