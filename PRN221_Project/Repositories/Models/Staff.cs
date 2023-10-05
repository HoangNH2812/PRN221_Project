using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string? StaffName { get; set; }

    public string? StaffPhone { get; set; }

    public int? StudioId { get; set; }

    public virtual ICollection<Login> Logins { get; set; } = new List<Login>();

    public virtual Studio? Studio { get; set; }
}
