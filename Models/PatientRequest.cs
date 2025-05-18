using System;
using System.Collections.Generic;

namespace Variant4.Models;

public partial class PatientRequest
{
    public int Id { get; set; }

    public string Article { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly CreatedDate { get; set; }

    public int Status { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
