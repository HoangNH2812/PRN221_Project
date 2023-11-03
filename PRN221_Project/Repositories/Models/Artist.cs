using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repositories.Models;

public partial class Artist
{
    public int ArtistId { get; set; }
    [MinLength(8), MaxLength(30)]
    public string? Fullname { get; set; }

    public string? Phone { get; set; }

    public int? StudioId { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();

    public virtual Studio? Studio { get; set; }

    public virtual ICollection<TattoosDesign> TattoosDesigns { get; set; } = new List<TattoosDesign>();
}
