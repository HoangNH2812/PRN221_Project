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
        public AppointmentDetailsModel(IAppointmentDetailRepository appointmentDetailRepository, IServiceRepository serviceRepository, IScheduleRepository scheduleRepository)
        {
            _appointmentDetailRepository = appointmentDetailRepository;
            _serviceRepository = serviceRepository;
            _scheduleRepository = scheduleRepository;
        }
        public IList<AppointmentDetail> AppointmentDetail { get; set; } = default!;
        public void OnGet(int? id)
        {
            AppointmentDetail = _appointmentDetailRepository.GetByAppointmentID(id.Value).ToList();
            foreach (var appointmentDetail in AppointmentDetail)
            {
                appointmentDetail.Service = _serviceRepository.GetByID(appointmentDetail.ServiceId.Value);
                appointmentDetail.Schedule = _scheduleRepository.GetByID(appointmentDetail.ScheduleId.Value);
            }
        }
    }
}
