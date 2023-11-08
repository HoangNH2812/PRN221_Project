using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.TattooLoverPage
{
    public class SelectScheduleModel : PageModel
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IServiceRepository _serviceRepository;
        public SelectScheduleModel(IScheduleRepository scheduleRepository, IServiceRepository serviceRepository)
        {
            _scheduleRepository = scheduleRepository;
            _serviceRepository = serviceRepository;
        }
        public IList<Schedule> Schedule { get; set; } = default!;

        public Service Service { get; set; }
        public IActionResult OnGet(int id)
        {
            Account account = HttpContext.Session.GetObjectFromJson<Account>("account");
            if (account == null)
            {
                return RedirectToPage("/LoginPage");
            }
            else if (account.TattooLoverId == null)
            {
                return RedirectToPage("/LoginPage");
            }
            Service = _serviceRepository.GetByID(id);
            int artistID; 
            if (Service.ArtistId == null)
            {
                return NotFound();
            }
            else { artistID = Service.ArtistId.Value; }
            Schedule = _scheduleRepository.GetSchedules(artistID, 0).ToList();
            return Page();
        }

        [BindProperty]
        public int serviceID { get; set; }
        
        [BindProperty]
        public int scheduleID { get; set; }
        public IActionResult OnPost() {
            Service service = _serviceRepository.GetByID(serviceID);
            AppointmentDetail appointmentDetail = new AppointmentDetail();
            appointmentDetail.ServiceId = service.ServiceId;
            appointmentDetail.Price = service.Price;
            appointmentDetail.ScheduleId = scheduleID;
            return RedirectToPage("./Cart", "AddtoCart", appointmentDetail);
        }
    }
}
