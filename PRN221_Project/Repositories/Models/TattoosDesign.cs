using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class TattoosDesign
{
    public int TattoosDesignId { get; set; }

    public string? TattoosDesignName { get; set; }

    public int? StyleId { get; set; }

    public string? ImgUri { get; set; }

    public string? Description { get; set; }

    public int? ArtistId { get; set; }

    public virtual Artist? Artist { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();

    public virtual Style? Style { get; set; }
}
