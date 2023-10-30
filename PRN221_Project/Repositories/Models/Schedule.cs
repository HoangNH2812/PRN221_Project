using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repositories.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }
    [DataType(DataType.Date)]
    public DateTime? Time { get; set; }

    public int? Status { get; set; }

    public int? ArtistId { get; set; }

    public virtual ICollection<AppointmentDetail> AppointmentDetails { get; set; } = new List<AppointmentDetail>();

    public virtual Artist? Artist { get; set; }
}
