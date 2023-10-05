using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class TattooLover
{
    public int TattooLoverId { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public int? Age { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Login> Logins { get; set; } = new List<Login>();
}
