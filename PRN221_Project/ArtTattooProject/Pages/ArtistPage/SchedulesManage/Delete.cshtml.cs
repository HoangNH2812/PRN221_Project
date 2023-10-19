using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.ArtistPage.SchedulesManage
{
    public class DeleteModel : PageModel
    {
        private readonly IScheduleRepository _scheduleRepository;

        public DeleteModel(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        [BindProperty]
        public Schedule Schedule { get; set; } = default!;

        public string Msg { get; set; }
        public  IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule =  _scheduleRepository.GetByID(id.Value);

            if (schedule == null)
            {
                return NotFound();
            }
            else 
            {
                Schedule = schedule;
            }
            return Page();
        }

        public  IActionResult OnPost(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }
            var schedule = _scheduleRepository.GetByID(id.Value);

            if (schedule != null)
            {
                if (schedule.Status == 1)
                {
                    Msg = "the schedule time has been booked, can not delete";
                    return Page();
                }
               _scheduleRepository.Delete(schedule);
            }

            return RedirectToPage("./Index");
        }
    }
}
