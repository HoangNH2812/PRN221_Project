using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Login
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? TattooLoverId { get; set; }

    public int? ArtistId { get; set; }

    public int? StaffId { get; set; }

    public virtual Artist? Artist { get; set; }

    public virtual Staff? Staff { get; set; }

    public virtual TattooLover? TattooLover { get; set; }
}
