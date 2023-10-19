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
        public IndexModel(IScheduleRepository scheduleRepository, IConfiguration configuration)
        {
           _scheduleRepository = scheduleRepository;
            Configuration = configuration;
        }

        public IQueryable<Schedule> ScheduleList { get;set; } = default!;
        public PaginatedList<Schedule> Schedule { get;set; } = default!;

        public void OnGet(int? pageIndex)
        {
            int artistId = HttpContext.Session.GetObjectFromJson<Account>("account").ArtistId.Value;
            ScheduleList = _scheduleRepository.GetSchedules(artistId,null).OrderByDescending(i=>i.Time).AsQueryable();
            var pageSize = Configuration.GetValue("PageSize", 4);
            Schedule = PaginatedList<Schedule>.Create(
                ScheduleList, pageIndex ?? 1, pageSize);
        }
    }
}
