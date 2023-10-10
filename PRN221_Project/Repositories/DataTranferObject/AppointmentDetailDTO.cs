using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DataTranferObject
{
    internal class AppointmentDetailDTO
    {
        public int AppointmentDetailId { get; set; }

        public decimal? Price { get; set; }

        public int? AppointmentId { get; set; }

        public int? ServiceId { get; set; }
    }
}
