using System;
using System.Collections.Generic;

namespace DayOff.Persistence.Entities;

public partial class Dy_Gender
{
    public decimal GenderId { get; set; }

    public string GenderName { get; set; } = null!;

    public virtual ICollection<Dy_DayOffType> DyDayOffTypes { get; set; } = new List<Dy_DayOffType>();

    public virtual ICollection<Dy_User> DyUsers { get; set; } = new List<Dy_User>();
}
