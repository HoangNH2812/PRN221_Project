using ArtTattooProject.Pages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.TattooLoverPage
{
    public class CartModel : PageModel
    {
        private readonly IAppointmentDetailRepository _appointmentDetailRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IStudioRepository _studioRepository;
        private readonly IScheduleRepository _scheduleRepository;
        public CartModel(IAppointmentDetailRepository appointmentDetailRepository, IServiceRepository serviceRepository, IAppointmentRepository appointmentRepository, IArtistRepository artistRepository, IStudioRepository studioRepository, IScheduleRepository scheduleRepository)
        {
            _appointmentDetailRepository = appointmentDetailRepository;
            _serviceRepository = serviceRepository;
            _appointmentRepository = appointmentRepository;
            _artistRepository = artistRepository;
            _studioRepository = studioRepository;
            _scheduleRepository = scheduleRepository;
        }
        public List<AppointmentDetail> cart { get; set; }
        public List<AppointmentDetail> appointmentDetail { get; set; }
        public decimal? Total { get; set; }
        public string Msg { get; set; }
        public void OnGet()
        {
            cart = SessionHelper.GetObjectFromJson<List<AppointmentDetail>>(HttpContext.Session, "cart");
            Total = 0;
            appointmentDetail = new List<AppointmentDetail>();
            if (cart != null)
            {
                foreach (var item in cart)
                {
                    if (item is AppointmentDetail)
                    {
                        AppointmentDetail temp = item;
                        Total += item.Price;
                        temp.Service = _serviceRepository.GetByID((int)item.ServiceId);
                        temp.Service.Artist = _artistRepository.GetByID((int)item.Service.ArtistId);
                        temp.Schedule = _scheduleRepository.GetByID((int) item.ScheduleId);
                        temp.Service.Artist.Studio = _studioRepository.GetByID((int)temp.Service.Artist.StudioId);
                        appointmentDetail.Add(temp);
                    }
                }
                HttpContext.Session.SetObjectAsJson("totalPrice", Total);
            }
        }

        public IActionResult OnGetAddtoCart(AppointmentDetail AppointmentDetail)
        {
            cart = SessionHelper.GetObjectFromJson<List<AppointmentDetail>>(HttpContext.Session, "cart");
            if (cart == null)
            {
                cart = new List<AppointmentDetail> {
                    AppointmentDetail
                };
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                int index = Exists(cart, AppointmentDetail.Service.ServiceId);
                if (index == -1)
                {
                    cart.Add(AppointmentDetail);
                }
                else
                {
                    cart[index] = AppointmentDetail;
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToPage("Cart");
        }
        public IActionResult OnGetDelete(int id)
        {
            int index = Exists(cart, id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToPage("Cart");
        }

        public IActionResult OnPost()
        {
            Appointment appointment = new Appointment();
            appointment.TotalPrice = HttpContext.Session.GetObjectFromJson<decimal>("totalPrice");
            appointment.TattooLoverId = HttpContext.Session.GetObjectFromJson<Account>("account").TattooLoverId;
            appointment.Status = 0;
            int? studioID = GetStudioIDInCart();
            if (cart.Count == 0 || cart==null) {
                Msg = "No service to book now";
                return Page();
            }
            if (studioID != null)
            {
                appointment.StudioId = studioID.Value;
            } else {
                Msg = "all service must from 1 studio";
                return Page();
            }
            int id = _appointmentRepository.AddNew(appointment);
            cart = SessionHelper.GetObjectFromJson<List<AppointmentDetail>>(HttpContext.Session, "cart");
            foreach (AppointmentDetail appointmentDetail in cart)
            {
                appointmentDetail.AppointmentId= id;
                _appointmentDetailRepository.AddNew(appointmentDetail);
            }
            cart = null;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToPage("Appointment");
        }

        public int? GetStudioIDInCart() {
            int? studioID;
            int? temp;
            cart = SessionHelper.GetObjectFromJson<List<AppointmentDetail>>(HttpContext.Session, "cart");
            if (cart.Count==1) {
                studioID = _GetStudioIDFromServiceID((int)cart[0].ServiceId);
            }
            else
            {
                studioID = _GetStudioIDFromServiceID((int) cart[0].ServiceId);
                foreach (var item in cart)
                {
                    temp = _GetStudioIDFromServiceID((int)item.ServiceId);
                    if (temp != studioID)
                    {
                        studioID = null;
                        break;
                    }
                }
            }
            return studioID;
        }

        public int _GetStudioIDFromServiceID(int serviceID) {
            int studioID;
            studioID = (int)_artistRepository.GetByID((int)_serviceRepository.GetByID(serviceID).ArtistId).StudioId;
            return studioID;
        }
        private int Exists(List<AppointmentDetail> cart, int id)
        {
            for (var i = 0; i < cart.Count; i++)
            {
                if (cart[i].ServiceId == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
