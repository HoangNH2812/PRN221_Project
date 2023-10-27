using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Repositories.IRepository;
using Repositories.Models;
using Repositories.Repository;
namespace ArtTattooProject.Pages
{
    public class LoginPageModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        public LoginPageModel(IAccountRepository repositoryBase)
        {
            _accountRepository = repositoryBase;
        }
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Msg { get; set; }
        public void OnGet()
        {

        }
        private Account GetAdminAccount()
        {
            Account admin = new Account();
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
            admin.Username = config["Admin:username"];
            admin.Password = config["Admin:password"];
            return admin;
        }
        public IActionResult OnPost()
        {

            try
            {
                Account admin = GetAdminAccount();
                Account account;
                account = _accountRepository.Login(Username, Password);
                if (account != null)
                {
                    HttpContext.Session.SetObjectAsJson("account", account);
                    if (account.ArtistId != null)
                    {
                        return RedirectToPage("/ArtistPage/Index");
                    }
                    else if (account.StaffId != null)
                    {
                        return RedirectToPage("/StaffPage/Index");
                    }
                    else
                    {
                        return RedirectToPage("/TattooLoverPage/Index");
                    }
                }
                else if (Username == admin.Username && Password == admin.Password)
                {
                    HttpContext.Session.SetObjectAsJson("account", admin);
                    HttpContext.Session.SetObjectAsJson("isAdmin",true);
                    return RedirectToPage("/AdminPage/Index");
                }
                else
                {
                    Msg = "Invalid";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                return RedirectToPage("/Error", ex);
            }
        }
    }
}
