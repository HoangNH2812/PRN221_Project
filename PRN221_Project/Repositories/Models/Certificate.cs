using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repositories.Models;

public partial class Certificate
{
    public int CertificateId { get; set; }
    [MinLength(1), MaxLength(50)]
    public string? CertificateName { get; set; }
}
