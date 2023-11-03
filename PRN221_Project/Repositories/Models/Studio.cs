using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Studio
{
    public int StudioId { get; set; }
    public int Status { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Website { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Artist> Artists { get; set; } = new List<Artist>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
