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

namespace ArtTattooProject.Pages.ArtistPage.SchedulesManage
{
    public class IndexModel : PageModel
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IConfiguration Configuration;
        private readonly IAppointmentDetailRepository _appointmentDetailRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ITattooLoverRepository _tattooLoverRepository;
        public IndexModel(IScheduleRepository scheduleRepository,
            IConfiguration configuration,
            IAppointmentDetailRepository appointmentDetailRepository,
            IAppointmentRepository appointmentRepository,
            ITattooLoverRepository tattooLoverRepository)
        {
            _scheduleRepository = scheduleRepository;
            Configuration = configuration;
            _appointmentDetailRepository = appointmentDetailRepository;
            _appointmentRepository = appointmentRepository;
            _tattooLoverRepository = tattooLoverRepository;
        }

        public IQueryable<ScheduleMapper> ScheduleList { get; set; } = default!;
        public PaginatedList<ScheduleMapper> Schedule { get; set; } = default!;

        public void OnGet(int? pageIndex)
        {
            int artistId = HttpContext.Session.GetObjectFromJson<Account>("account").ArtistId.Value;
            IEnumerable<Schedule> schedules = _scheduleRepository.GetSchedules(artistId, null).OrderByDescending(i => i.Time);
            List<ScheduleMapper> list = new List<ScheduleMapper>();
            foreach (Schedule schedule in schedules)
            {
                if (schedule.Status == 1)
                {
                    AppointmentDetail detail = _appointmentDetailRepository.GetByScheduleID(schedule.ScheduleId);
                    Appointment appointment = _appointmentRepository.GetByID(detail.AppointmentId.Value);
                    TattooLover tattooLover = _tattooLoverRepository.GetByID(appointment.TattooLoverId.Value);
                    list.Add(new ScheduleMapper(schedule,tattooLover));
                }
                else
                {
                    list.Add(new ScheduleMapper(schedule,null));
                }
            }
            ScheduleList = list.AsQueryable();
            var pageSize = Configuration.GetValue("PageSize", 4);
            Schedule = PaginatedList<ScheduleMapper>.Create(
                ScheduleList, pageIndex ?? 1, pageSize);
        }
    }
    public class ScheduleMapper
    {
        public Schedule Schedule { get; set; }
        public TattooLover? TattooLover { get; set; }
        public ScheduleMapper(Schedule schedule, TattooLover? tattooLover)
        {
            Schedule = schedule;
            TattooLover = tattooLover;
        }
    }
}
