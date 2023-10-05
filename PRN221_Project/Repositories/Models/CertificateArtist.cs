using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class CertificateArtist
{
    public int? CertificateId { get; set; }

    public int? ArtistId { get; set; }

    public virtual Artist? Artist { get; set; }

    public virtual Certificate? Certificate { get; set; }
}
