using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repositories.Models;

public partial class Account
{
    [MinLength(8), MaxLength(30)]
    public string Username { get; set; } = null!;
    [MinLength(8), MaxLength(30)]
    public string Password { get; set; } = null!;

    public int Status { get; set; }

    public int? TattooLoverId { get; set; }

    public int? ArtistId { get; set; }

    public int? StaffId { get; set; }

    public virtual Artist? Artist { get; set; }

    public virtual Staff? Staff { get; set; }

    public virtual TattooLover? TattooLover { get; set; }
}
