using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string? StaffName { get; set; }

    public string? StaffPhone { get; set; }

    public int? StudioId { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual Studio? Studio { get; set; }
}
