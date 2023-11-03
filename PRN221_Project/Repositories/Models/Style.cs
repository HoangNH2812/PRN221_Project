using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repositories.Models;

public partial class Style
{
    public int StyleId { get; set; }
    [MinLength(6), MaxLength(30)]
    public string? StyleName { get; set; }

    public virtual ICollection<TattoosDesign> TattoosDesigns { get; set; } = new List<TattoosDesign>();
}
