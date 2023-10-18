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
        public AppointmentModel(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public IList<Appointment> Appointment { get; set; } = default!;
        public void OnGet()
        {
            int TattooLoverID = HttpContext.Session.GetObjectFromJson<Account>("account").TattooLoverId.Value;
            Appointment = _appointmentRepository.GetByTattooLover(TattooLoverID).ToList();
        }
    }
}
