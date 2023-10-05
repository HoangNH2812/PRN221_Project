using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public DateTime? Time { get; set; }

    public int? ArtistId { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Artist? Artist { get; set; }
}
