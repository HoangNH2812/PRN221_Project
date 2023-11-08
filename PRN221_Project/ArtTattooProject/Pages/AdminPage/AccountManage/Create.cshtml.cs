using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.AdminPage.AccountManage
{
    public class CreateModel : PageModel
    {
        private readonly IStudioRepository _studioRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IStaffRepository _staffRepository;
        public CreateModel(IAccountRepository accountRepository, IStaffRepository staffRepository, IStudioRepository studioRepository)
        {
            _accountRepository = accountRepository;
            _staffRepository = staffRepository;
            _studioRepository = studioRepository;
        }

        public IActionResult OnGet()
        {
            Account account = HttpContext.Session.GetObjectFromJson<Account>("account");
            if (account == null)
            {
                return RedirectToPage("../LoginPage");
            }
            else {
                string isAdmin = HttpContext.Session.GetString("isAdmin");
                if (isAdmin==null || isAdmin == "")
                {
                    return RedirectToPage("../LoginPage");
                }
                bool isADMIN = JsonConvert.DeserializeObject<Boolean>(isAdmin);
                if (!isADMIN)
                {
                    return RedirectToPage("../LoginPage");
                }
            }
            ViewData["StudioId"] = new SelectList(_studioRepository.GetAll(), "StudioId", "Name");
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; } = default!;
        [BindProperty]
        public Staff Staff { get; set; } = default!;
        [BindProperty]
        public string? Msg { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public  IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return OnGet();
            }

            try
            {
                int staffId = _staffRepository.AddNew(Staff);
                if (staffId > 0) { 
                    Account.StaffId= staffId;
                    try
                    {
                        _accountRepository.AddNew(Account);
                    } catch (Exception ex)
                    {
                        _staffRepository.Delete(Staff);
                        Msg = ex.Message;
                        return OnGet();
                    }
                }
            }
            catch (Exception ex)
            {
                Msg = ex.Message;
                return OnGet();
            }

            return RedirectToPage("./Index");
        }
    }
}
