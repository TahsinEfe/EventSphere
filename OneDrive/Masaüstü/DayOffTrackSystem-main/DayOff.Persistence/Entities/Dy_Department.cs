using System;
using System.Collections.Generic;

namespace DayOff.Persistence.Entities;

public partial class Dy_Department
{
    public decimal DepId { get; set; }

    public string DepName { get; set; } = null!;

    public virtual ICollection<Dy_User> DyUsers { get; set; } = new List<Dy_User>();
}
