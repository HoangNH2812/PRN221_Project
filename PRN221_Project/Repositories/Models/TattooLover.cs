using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repositories.Models;

public partial class TattooLover
{
    public int TattooLoverId { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }
    [Range(18, 120)]
    public int? Age { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
