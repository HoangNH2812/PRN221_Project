using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DataTranferObject
{
    internal class LoginDTO
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int? TattooLoverId { get; set; }

        public int? ArtistId { get; set; }

        public int? StaffId { get; set; }
    }
}
