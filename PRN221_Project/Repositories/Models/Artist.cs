using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Artist
{
    public int ArtistId { get; set; }

    public string? Fullname { get; set; }

    public int? MainStyle { get; set; }

    public string? Phone { get; set; }

    public int? StudioId { get; set; }

    public virtual ICollection<Login> Logins { get; set; } = new List<Login>();

    public virtual Style? MainStyleNavigation { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual Studio? Studio { get; set; }

    public virtual ICollection<TattoosDesign> TattoosDesigns { get; set; } = new List<TattoosDesign>();
}
