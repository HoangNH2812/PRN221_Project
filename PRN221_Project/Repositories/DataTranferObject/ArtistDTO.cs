using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DataTranferObject
{
    internal class ArtistDTO
    {
        public int ArtistId { get; set; }

        public string? Fullname { get; set; }

        public int? MainStyle { get; set; }

        public string? Phone { get; set; }

        public int? StudioId { get; set; }
    }
}
