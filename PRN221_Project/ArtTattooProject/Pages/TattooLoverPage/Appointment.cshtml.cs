using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.TattooLoverPage
{
    public class AppointmentModel : PageModel
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IConfiguration Configuration;
        private readonly IStudioRepository _studioRepository;
        public AppointmentModel(IAppointmentRepository appointmentRepository, IConfiguration configuration, IStudioRepository studioRepository)
        {
            _appointmentRepository = appointmentRepository;
            Configuration = configuration;
            _studioRepository = studioRepository;
        }
        public IQueryable<Appointment> AppointmentList { get; set; } = default!;
        public PaginatedList<Appointment> Appointment { get; set; } = default!;

        public void OnGet(int? pageIndex)
        {
            int TattooLoverID = HttpContext.Session.GetObjectFromJson<Account>("account").TattooLoverId.Value;
            AppointmentList = _appointmentRepository.GetByTattooLover(TattooLoverID).AsQueryable();
            foreach (var item in AppointmentList)
            {
                item.Studio = _studioRepository.GetByID(item.StudioId.Value);
            }
            var pageSize = Configuration.GetValue("PageSize", 4);
            Appointment = PaginatedList<Appointment>.Create(
                AppointmentList, pageIndex ?? 1, pageSize);
        }
        [BindProperty]
        public int cancelId { get; set; }
        public void OnPostCancelAppointment()
        {
            Appointment appointment = _appointmentRepository.GetByID(cancelId);
            appointment.Status = 4;
            _appointmentRepository.Update(appointment);
            OnGet(null);
        }
    }
}
