using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DataTranferObject
{
    internal class StaffDTO
    {
        public int StaffId { get; set; }

        public string? StaffName { get; set; }

        public string? StaffPhone { get; set; }

        public int? StudioId { get; set; }
    }
}
