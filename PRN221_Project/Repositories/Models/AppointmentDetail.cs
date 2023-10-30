using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repositories.Models;

public partial class AppointmentDetail
{
    public int AppointmentDetailId { get; set; }
    [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = false)]
    public decimal? Price { get; set; }

    public int? AppointmentId { get; set; }

    public int? ServiceId { get; set; }

    public int? ScheduleId { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual Schedule? Schedule { get; set; }

    public virtual Service? Service { get; set; }
}
