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

namespace ArtTattooProject.Pages.ArtistPage.SchedulesManage
{
    public class CreateModel : PageModel
    {
        private readonly IScheduleRepository _scheduleRepository;

        public CreateModel(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public IActionResult OnGet()
        {
            Account account = HttpContext.Session.GetObjectFromJson<Account>("account");
            if (account == null)
            {
                return RedirectToPage("../LoginPage");
            }
            else if (account.ArtistId == null)
            {
                return RedirectToPage("../LoginPage");
            }
            return Page();
        }

        [BindProperty]
        public Schedule Schedule { get; set; } = default!;
        public string Msg { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public  IActionResult OnPost()
        {
          if (!ModelState.IsValid || Schedule == null)
            {
                return Page();
            }
            int artistId = HttpContext.Session.GetObjectFromJson<Account>("account").ArtistId.Value;
            Schedule.ArtistId = artistId;
            Schedule.Status = 0;
            try
            {
                _scheduleRepository.AddNew(Schedule);
            } catch (Exception ex)
            {
                Msg = ex.Message;
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
