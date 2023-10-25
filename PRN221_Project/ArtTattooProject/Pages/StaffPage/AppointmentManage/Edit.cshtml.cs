using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.StaffPage.AppointmentManage
{
    public class EditModel : PageModel
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ITattooLoverRepository _tattooLoverRepository;

        public EditModel(IAppointmentRepository appointment, ITattooLoverRepository tattooLoverRepository)
        {
            _appointmentRepository = appointment;
            _tattooLoverRepository = tattooLoverRepository;
        }

        [BindProperty]
        public Appointment Appointment { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var appointment =  _appointmentRepository.GetByID(int.Parse(id.ToString()));
            if (appointment == null)
            {
                return NotFound();
            }
            Appointment = appointment;
            IEnumerable<object> status = new[] { new { Id = 1, name = "waiting" }, new { Id = 2, name = "progress" }, new { Id = 3, name = "done" }, new { Id = 4, name = "canceled" } };
            ViewData["Status"] = new SelectList(status, "Id", "name");
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
                Appointment appointment = _appointmentRepository.GetByID(Appointment.AppointmentId);
                appointment.Status=Appointment.Status;
                _appointmentRepository.Update(appointment);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(Appointment.AppointmentId))
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

        private bool AppointmentExists(int id)
        {
          return (_appointmentRepository.GetByID(id)==null);
        }
    }
}
