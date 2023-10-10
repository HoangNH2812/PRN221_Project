using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DataTranferObject
{
    internal class AppointmentDTO
    {
        public int AppointmentId { get; set; }

        public decimal? TotalPrice { get; set; }

        public int? Status { get; set; }

        public int? TattooLoverId { get; set; }

        public int? ScheduleId { get; set; }

        public int? StudioId { get; set; }
    }
}
