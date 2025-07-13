using System;
using System.Collections.Generic;

namespace DayOff.Persistence.Entities;

public partial class Dy_Role
{
    public decimal RoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<Dy_User> DyUsers { get; set; } = new List<Dy_User>();
}
