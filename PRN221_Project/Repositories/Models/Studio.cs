using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repositories.Models;

public partial class Studio
{
    public int StudioId { get; set; }
    public int Status { get; set; }
    [MinLength(8), MaxLength(30)]
    public string? Name { get; set; }
    [MinLength(8), MaxLength(80)]
    public string? Address { get; set; }

    public string? Phone { get; set; }
    [MinLength(8), MaxLength(50)]
    public string? Website { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Artist> Artists { get; set; } = new List<Artist>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
