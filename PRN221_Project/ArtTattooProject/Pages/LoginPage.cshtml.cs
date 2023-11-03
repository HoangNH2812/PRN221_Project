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
        private readonly IStudioRepository _studioRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IStaffRepository _staffRepository;
        public LoginPageModel(IAccountRepository repositoryBase, IStudioRepository studioRepository, IArtistRepository artistRepository, IStaffRepository staffRepository)
        {
            _accountRepository = repositoryBase;
            _studioRepository = studioRepository;
            _artistRepository = artistRepository;
            _staffRepository = staffRepository;
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
                if (Username== null || Password==null) {
                    Msg = "Must enter username and password";
                    return Page();
                }
                Account admin = GetAdminAccount();
                Account account;
                account = _accountRepository.Login(Username, Password);
                if (account != null)
                {
                    if (account.Status == 0)
                    {
                        Msg = "this account has been lock, contact admin page to unlock";
                        return Page();
                    }
                    HttpContext.Session.SetObjectAsJson("account", account);
                    HttpContext.Session.SetObjectAsJson("isAdmin", false);
                    if (account.TattooLoverId != null)
                    {
                        return RedirectToPage("/TattooLoverPage/Index");
                    } else if (account.ArtistId != null)
                    {
                        int studioID = _artistRepository.GetByID(account.ArtistId.Value).StudioId.Value;
                        if (_studioRepository.GetByID(studioID).Status == 0)
                        {
                            Msg = "Studio you belong has been lock, contact admin for more infomation";
                            return Page();
                        }
                        return RedirectToPage("/ArtistPage/Index");
                    }
                    else
                    {
                        int studioID = _staffRepository.GetByID(account.StaffId.Value).StudioId.Value;
                        if (_studioRepository.GetByID(studioID).Status == 0)
                        {
                            Msg = "Studio you belong has been lock, contact admin for more infomation";
                            return Page();
                        }
                        return RedirectToPage("/StaffPage/Index");
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
                    Msg = "Wrong usernane or password";
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
