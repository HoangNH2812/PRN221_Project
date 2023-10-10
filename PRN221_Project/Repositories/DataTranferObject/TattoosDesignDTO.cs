using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DataTranferObject
{
    internal class TattoosDesignDTO
    {
        public int TattoosDesignId { get; set; }

        public string? TattoosDesignName { get; set; }

        public int? StyleId { get; set; }

        public string? ImgUri { get; set; }

        public string? Description { get; set; }

        public int? ArtistId { get; set; }
    }
}
