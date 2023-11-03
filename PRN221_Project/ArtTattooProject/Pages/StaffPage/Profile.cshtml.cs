using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.StaffPage
{
    public class ProfileModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IStaffRepository _staffRepository;
        public ProfileModel(IAccountRepository accountRepository, IStaffRepository staffRepository)
        {
            _accountRepository = accountRepository;
            _staffRepository = staffRepository;
        }
        public string Msg { get; set; }
        [BindProperty]
        public string? newPassword { get; set; }
        [BindProperty]
        public string? confirmPassword { get; set; }
        [BindProperty]
        public string password { get; set; }
        [BindProperty]
        public Staff Staff { get; set; }
        public IActionResult OnGet()
        {
            int id = HttpContext.Session.GetObjectFromJson<Account>("account").StaffId.Value;
            Staff = _staffRepository.GetByID(id);
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
                Staff.StaffId = account.StaffId.Value;
                _staffRepository.Update(Staff);
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
