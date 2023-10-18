using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public DateTime? Time { get; set; }

    public int? Status { get; set; }

    public int? ArtistId { get; set; }

    public virtual ICollection<AppointmentDetail> AppointmentDetails { get; set; } = new List<AppointmentDetail>();

    public virtual Artist? Artist { get; set; }
}
