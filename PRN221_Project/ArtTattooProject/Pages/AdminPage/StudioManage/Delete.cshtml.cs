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

namespace ArtTattooProject.Pages.AdminPage.StudioManage
{
    public class DeleteModel : PageModel
    {
        private readonly IStudioRepository _studioRepository;

        public DeleteModel(IStudioRepository studioRepository)
        {
            _studioRepository = studioRepository;
        }

        [BindProperty]
        public Studio Studio { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            Account account = HttpContext.Session.GetObjectFromJson<Account>("account");
            if (account == null)
            {
                return RedirectToPage("/LoginPage");
            }
            else
            {
                string isAdmin = HttpContext.Session.GetString("isAdmin");
                if (isAdmin == null || isAdmin == "")
                {
                    return RedirectToPage("/LoginPage");
                }
                bool isADMIN = JsonConvert.DeserializeObject<Boolean>(isAdmin);
                if (!isADMIN)
                {
                    return RedirectToPage("/LoginPage");
                }
            }
            if (id == null)
            {
                return NotFound();
            }

            var studio = _studioRepository.GetByID(id.Value);

            if (studio == null)
            {
                return NotFound();
            }
            else
            {
                Studio = studio;
            }
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studio = _studioRepository.GetByID(id.Value);

            if (studio != null)
            {
                Studio = studio;
                if (Studio.Status == 1)
                {
                    Studio.Status = 0;
                }
                else if (Studio.Status == 0)
                {
                    Studio.Status = 1;
                }
                _studioRepository.Update(Studio);
            }
            return RedirectToPage("./Index");
        }
    }
}
