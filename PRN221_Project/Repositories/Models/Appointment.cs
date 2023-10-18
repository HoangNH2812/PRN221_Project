using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public decimal? TotalPrice { get; set; }

    public int? Status { get; set; }

    public int? TattooLoverId { get; set; }

    public int? StudioId { get; set; }

    public virtual ICollection<AppointmentDetail> AppointmentDetails { get; set; } = new List<AppointmentDetail>();

    public virtual Studio? Studio { get; set; }

    public virtual TattooLover? TattooLover { get; set; }
}
