using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;
using Repositories.Models;
using Repositories.Repository;

namespace ArtTattooProject.Pages.TattooLoverPage
{
    public class AppointmentModel : PageModel
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IConfiguration Configuration;
        private readonly IStudioRepository _studioRepository;
        private readonly IAppointmentDetailRepository _appointmentDetailRepository;
        private readonly IScheduleRepository _scheduleRepository;
        public AppointmentModel(IAppointmentRepository appointmentRepository, IConfiguration configuration, IStudioRepository studioRepository, IAppointmentDetailRepository appointmentDetailRepository, IScheduleRepository scheduleRepository)
        {
            _appointmentRepository = appointmentRepository;
            Configuration = configuration;
            _studioRepository = studioRepository;
            _appointmentDetailRepository = appointmentDetailRepository;
            _scheduleRepository = scheduleRepository;
        }
        public IQueryable<Appointment> AppointmentList { get; set; } = default!;
        public PaginatedList<Appointment> Appointment { get; set; } = default!;

        public IActionResult OnGet(int? pageIndex)
        {
            Account account = HttpContext.Session.GetObjectFromJson<Account>("account");
            if (account == null)
            {
                return RedirectToPage("/LoginPage");
            } else if (account.TattooLoverId == null)
            {
                return RedirectToPage("/LoginPage");
            }
            int TattooLoverID = HttpContext.Session.GetObjectFromJson<Account>("account").TattooLoverId.Value;
            AppointmentList = _appointmentRepository.GetByTattooLover(TattooLoverID).AsQueryable();
            foreach (var item in AppointmentList)
            {
                item.Studio = _studioRepository.GetByID(item.StudioId.Value);
            }
            var pageSize = Configuration.GetValue("PageSize", 4);
            Appointment = PaginatedList<Appointment>.Create(
                AppointmentList, pageIndex ?? 1, pageSize);
            return Page();
        }
        [BindProperty]
        public int cancelId { get; set; }
        public void OnPostCancelAppointment()
        {
            Appointment appointment = _appointmentRepository.GetByID(cancelId);
            appointment.Status = 4;
            _appointmentRepository.Update(appointment);
            ReleaseRecollection(cancelId);
            OnGet(null);
        }
        private void ReleaseRecollection(int appointmentId)
        {
            IList<AppointmentDetail> appointmentDetails = _appointmentDetailRepository.GetByAppointmentID(appointmentId).ToList();
            foreach (AppointmentDetail item in appointmentDetails)
            {
                Schedule schedule = _scheduleRepository.GetByID(item.ScheduleId.Value);
                schedule.Status = 0;
                _scheduleRepository.Update(schedule);
            }
        }
    }
}
