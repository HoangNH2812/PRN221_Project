using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DataTranferObject
{
    internal class ScheduleDTO
    {
        public int ScheduleId { get; set; }

        public DateTime? Time { get; set; }

        public int? ArtistId { get; set; }
    }
}
