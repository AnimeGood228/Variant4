using System;
using System.Collections.Generic;

namespace Variant4.Models;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateOnly RegisteredDate { get; set; }

    public string FullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<PatientRequest> PatientRequests { get; set; } = new List<PatientRequest>();
}
