using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repositories.Models;

public partial class Service
{
    public int ServiceId { get; set; }
    [MinLength(6), MaxLength(50)]
    public string? ServiceName { get; set; }
    [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = false)]
    [Range(0, Double.PositiveInfinity)]
    public decimal? Price { get; set; }

    public int? ArtistId { get; set; }

    public int? TattoosDesignId { get; set; }

    public virtual ICollection<AppointmentDetail> AppointmentDetails { get; set; } = new List<AppointmentDetail>();

    public virtual Artist? Artist { get; set; }

    public virtual TattoosDesign? TattoosDesign { get; set; }
}
