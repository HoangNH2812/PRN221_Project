using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.AdminPage.AccountManage
{
    public class DeleteModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        public DeleteModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [BindProperty]
      public Account Account { get; set; } = default!;

        public IActionResult OnGet(string username)
        {
            Account accountAuthen = HttpContext.Session.GetObjectFromJson<Account>("account");
            if (accountAuthen == null)
            {
                return RedirectToPage("../LoginPage");
            }
            else
            {
                string isAdmin = HttpContext.Session.GetString("isAdmin");
                if (isAdmin == null || isAdmin == "")
                {
                    return RedirectToPage("../LoginPage");
                }
                bool isADMIN = JsonConvert.DeserializeObject<Boolean>(isAdmin);
                if (!isADMIN)
                {
                    return RedirectToPage("../LoginPage");
                }
            }

            if (username == null )
            {
                return NotFound();
            }

           var account = _accountRepository.GetByUsername(username);

            if (account == null)
            {
                return NotFound();
            }
            else
            {
                Account = account;
            }
            return Page();
        }

        public  IActionResult OnPost(string username)
        {
            if (username == null)
            {
                return NotFound();
            }
            var account = _accountRepository.GetByUsername(username);

            if (account != null)
            {
                Account = account;
                if (Account.Status==1) account.Status = 0; else account.Status = 1;
                _accountRepository.Update(Account);
            }

            return RedirectToPage("./Index");
        }
    }
}
