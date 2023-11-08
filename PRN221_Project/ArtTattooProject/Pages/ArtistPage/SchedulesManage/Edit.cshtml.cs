using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.ArtistPage.SchedulesManage
{
    public class EditModel : PageModel
    {
        private readonly IScheduleRepository _scheduleRepository;

        public EditModel(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        [BindProperty]
        public Schedule Schedule { get; set; } = default!;

        public  IActionResult OnGet(int? id)
        {
            Account account = HttpContext.Session.GetObjectFromJson<Account>("account");
            if (account == null)
            {
                return RedirectToPage("/LoginPage");
            }
            else if (account.ArtistId == null)
            {
                return RedirectToPage("/LoginPage");
            }
            if (id == null)
            {
                return NotFound();
            }

            var schedule = _scheduleRepository.GetByID(id.Value);
            if (schedule == null)
            {
                return NotFound();
            }
            Schedule = schedule;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public  IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            try
            {
                Schedule oldSchedule = _scheduleRepository.GetByID(Schedule.ScheduleId);
                Schedule.ArtistId = oldSchedule.ArtistId;
                Schedule.Status = oldSchedule.Status;
                _scheduleRepository.Update(Schedule);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(Schedule.ScheduleId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ScheduleExists(int id)
        {
          return (_scheduleRepository.GetByID(id)==null);
        }
    }
}
