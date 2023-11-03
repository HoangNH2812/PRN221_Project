using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.ArtistPage
{
    public class ProfileModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IArtistRepository _artistRepository;
        public ProfileModel(IAccountRepository accountRepository, IArtistRepository artistRepository)
        {
            _accountRepository = accountRepository;
            _artistRepository = artistRepository;
        }
        public string Msg { get; set; }
        [BindProperty]
        public string? newPassword { get; set; }
        [BindProperty]
        public string? confirmPassword { get; set; }
        [BindProperty]
        public string password { get; set; }
        [BindProperty]
        public Artist Artist { get; set; }
        public IActionResult OnGet()
        {
            int id = HttpContext.Session.GetObjectFromJson<Account>("account").ArtistId.Value;
            Artist = _artistRepository.GetByID(id);
            return Page();
        }
        public IActionResult OnPost()
        {
            Account account = HttpContext.Session.GetObjectFromJson<Account>("account");
            if (!account.Password.Equals(password))
            {
                Msg = "wrong Password, you cannot save your profile";
                return OnGet();
            }
            try
            {
                if (newPassword != null && newPassword != "")
                {
                    if (newPassword.Equals(confirmPassword))
                    {
                        account.Password = newPassword;
                        _accountRepository.Update(account);
                        HttpContext.Session.SetObjectAsJson("account", account);
                    }
                }
                Artist.ArtistId = account.StaffId.Value;
                _artistRepository.Update(Artist);
                Msg = "update successful";
            }
            catch (Exception ex)
            {
                Msg = ex.Message;
            }
            return OnGet();
        }
    }
}
