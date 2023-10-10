using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DataTranferObject
{
    internal class TattooLoverDTO
    {
        public int TattooLoverId { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public int? Age { get; set; }
    }
}
