using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepository;
using Repositories.Models;

namespace ArtTattooProject.Pages.StaffPage.AppointmentManage
{
    public class DetailsModel : PageModel
    {
        private readonly IAppointmentDetailRepository _appointmentDetailRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IConfiguration Configuration;
        private readonly IServiceRepository _serviceRepository;

        public DetailsModel(IAppointmentDetailRepository appointmentDetailRepository, IAppointmentRepository appointmentRepository, IConfiguration configuration, IServiceRepository serviceRepository)
        {
            _appointmentDetailRepository = appointmentDetailRepository;
            _appointmentRepository = appointmentRepository;
            Configuration = configuration;
            _serviceRepository = serviceRepository;
        }

        public IQueryable<AppointmentDetail> AppointmentDetailList { get; set; } = default!;
        public PaginatedList<AppointmentDetail> AppointmentDetail { get; set; } = default!;
        public Appointment Appointment { get; set; }
        public IActionResult OnGet(int? id,int? pageIndex)
        {
            if (id == null)
            {
                return NotFound();
            }
            var appointment = _appointmentRepository.GetByID(id.Value);
            var appointmentDetails = _appointmentDetailRepository.GetByAppointmentID(int.Parse(id.ToString()));
            foreach ( var item in appointmentDetails )
            {
                item.Service = _serviceRepository.GetByID(item.ServiceId.Value);
            }
            if (appointmentDetails == null || appointment == null)
            {
                return NotFound();
            }
            else
            {
                Appointment = appointment;
                AppointmentDetailList = appointmentDetails.AsQueryable();

                var pageSize = Configuration.GetValue("PageSize", 4);
                AppointmentDetail = PaginatedList<AppointmentDetail>.Create(
                    AppointmentDetailList, pageIndex ?? 1, pageSize);
            }
            return Page();
        }
    }
}
