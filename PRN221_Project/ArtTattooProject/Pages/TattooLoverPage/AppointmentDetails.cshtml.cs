using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.TattooLoverPage
{
    public class AppointmentDetailsModel : PageModel
    {
        private readonly IAppointmentDetailRepository _appointmentDetailRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentDetailsModel(IAppointmentDetailRepository appointmentDetailRepository, IServiceRepository serviceRepository, IScheduleRepository scheduleRepository, IAppointmentRepository appointmentRepository)
        {
            _appointmentDetailRepository = appointmentDetailRepository;
            _serviceRepository = serviceRepository;
            _scheduleRepository = scheduleRepository;
            _appointmentRepository = appointmentRepository;
        }
        public IList<AppointmentDetail> AppointmentDetail { get; set; } = default!;
        public decimal totalPrice {  get; set; }
        public IActionResult OnGet(int? id)
        {
            Account account = HttpContext.Session.GetObjectFromJson<Account>("account");
            if (account == null)
            {
                return RedirectToPage("../LoginPage");
            }
            else if (account.TattooLoverId == null)
            {
                return RedirectToPage("../LoginPage");
            }
            totalPrice = _appointmentRepository.GetByID(id.Value).TotalPrice.Value;
            AppointmentDetail = _appointmentDetailRepository.GetByAppointmentID(id.Value).ToList();
            foreach (var appointmentDetail in AppointmentDetail)
            {
                appointmentDetail.Service = _serviceRepository.GetByID(appointmentDetail.ServiceId.Value);
                appointmentDetail.Schedule = _scheduleRepository.GetByID(appointmentDetail.ScheduleId.Value);
            }

            return Page();
        }
    }
}
