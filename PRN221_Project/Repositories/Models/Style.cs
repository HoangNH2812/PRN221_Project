using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Style
{
    public int StyleId { get; set; }

    public string? StyleName { get; set; }

    public virtual ICollection<TattoosDesign> TattoosDesigns { get; set; } = new List<TattoosDesign>();
}
