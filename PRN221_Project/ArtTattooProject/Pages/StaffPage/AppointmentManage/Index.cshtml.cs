using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.StaffPage.AppointmentManage
{
    public class IndexModel : PageModel
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IStudioRepository _studioRepository;
        private readonly ITattooLoverRepository _tattooLoverRepository;
        private readonly IConfiguration Configuration;
        private readonly IStaffRepository _staffRepository;
        public IndexModel(IAppointmentRepository appointmentRepository, IConfiguration configuration, IStudioRepository studioRepository, ITattooLoverRepository tattooLoverRepository, IStaffRepository staffRepository)
        {
            _appointmentRepository = appointmentRepository;
            Configuration = configuration;
            _studioRepository = studioRepository;
            _tattooLoverRepository = tattooLoverRepository;
            _staffRepository = staffRepository;
        }

        public IQueryable<Appointment> AppointmentList { get;set; } = default!;
        public PaginatedList<Appointment> Appointment { get;set; } = default!;

        public IActionResult OnGet(int? pageIndex)
        {
            Account account = HttpContext.Session.GetObjectFromJson<Account>("account");
            if (account == null)
            {
                return RedirectToPage("/LoginPage");
            }
            else if (account.StaffId == null)
            {
                return RedirectToPage("/LoginPage");
            }
            int staffId = HttpContext.Session.GetObjectFromJson<Account>("account").StaffId.Value;
            int studioId = _staffRepository.GetByID(staffId).StudioId.Value;
            Studio studio = _studioRepository.GetByID(studioId);
            IEnumerable<Appointment> appointments = _appointmentRepository.GetByStudio(studioId);
            foreach (Appointment appointment in appointments)
            {
                appointment.Studio=studio;
                appointment.TattooLover = _tattooLoverRepository.GetByID(appointment.TattooLoverId.Value);
            }
            AppointmentList = appointments.AsQueryable();

            var pageSize = Configuration.GetValue("PageSize", 4);
            Appointment = PaginatedList<Appointment>.Create(
                AppointmentList, pageIndex ?? 1, pageSize);

            return Page();
        }
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/LoginPage");
        }
    }
}
