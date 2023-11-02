using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;
using Repositories.Models;
using System.ComponentModel.DataAnnotations;

namespace ArtTattooProject.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITattooLoverRepository _tattooLoverRepository;

        public SignUpModel(IAccountRepository accountRepository, ITattooLoverRepository tattooLoverRepository)
        {
            _accountRepository = accountRepository;
            _tattooLoverRepository = tattooLoverRepository;
        }
        [BindProperty]
        public Account Account { get; set; }
        [BindProperty]
        public TattooLover TattooLover { get; set; }

        [BindProperty]
        public string confirmPassword { get; set; }
        [BindProperty]
        public string? Msg { get; set; }

        public void OnGet() {

        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Msg = "invalid input";
                return Page();
            }
            if (Account != null && TattooLover != null)
            {
                if (confirmPassword!= Account.Password)
                {
                    Msg = "confirm password must same as password";
                    return Page();
                }
                try
                {
                    int id = _tattooLoverRepository.AddNew(TattooLover);
                    Account.TattooLoverId = id;
                    try
                    {
                        _accountRepository.AddNew(Account);
                    }
                    catch (Exception ex)
                    {
                        _tattooLoverRepository.Delete(TattooLover);
                        Msg = ex.Message;
                        return Page();
                    }
                }
                catch (Exception ex)
                {
                    Msg = ex.Message;
                    return Page();
                }
            }
            return RedirectToPage("LoginPage");
        }

    }
}
