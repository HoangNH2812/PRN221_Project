using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class AppointmentDetail
{
    public int AppointmentDetailId { get; set; }

    public decimal? Price { get; set; }

    public int? AppointmentId { get; set; }

    public int? ServiceId { get; set; }

    public int? ScheduleId { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual Schedule? Schedule { get; set; }

    public virtual Service? Service { get; set; }
}
