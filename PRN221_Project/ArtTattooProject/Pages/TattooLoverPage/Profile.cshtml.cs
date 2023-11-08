using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.TattooLoverPage
{
    public class ProfileModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITattooLoverRepository _tattooLoverRepository;
        public ProfileModel(IAccountRepository accountRepository, ITattooLoverRepository tattooLoverRepository)
        {
            _accountRepository = accountRepository;
            _tattooLoverRepository = tattooLoverRepository;
        }
        public string Msg { get; set; }
        [BindProperty]
        public string? newPassword { get; set; }
        [BindProperty]
        public string? confirmPassword { get; set; }
        [BindProperty]
        public string password { get; set; }
        [BindProperty]
        public TattooLover TattooLover { get; set; }
        public IActionResult OnGet()
        {
            Account account = HttpContext.Session.GetObjectFromJson<Account>("account");
            if (account == null)
            {
                return RedirectToPage("/LoginPage");
            }
            else if (account.TattooLoverId == null)
            {
                return RedirectToPage("/LoginPage");
            }
            int id = HttpContext.Session.GetObjectFromJson<Account>("account").TattooLoverId.Value;
            TattooLover = _tattooLoverRepository.GetByID(id);
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
                        HttpContext.Session.SetObjectAsJson("account",account);
                    }
                }
                TattooLover.TattooLoverId=account.TattooLoverId.Value; 
                _tattooLoverRepository.Update(TattooLover);
                Msg = "update successful";
            } catch (Exception ex)
            {
                Msg = ex.Message;
            }
            return OnGet();
        }
    }
}
