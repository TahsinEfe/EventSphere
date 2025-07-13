using System;
using System.Collections.Generic;

namespace DayOff.Persistence.Entities;

public partial class Dy_Title
{
    public decimal TitleId { get; set; }

    public string TitleName { get; set; } = null!;

    public virtual ICollection<Dy_User> DyUsers { get; set; } = new List<Dy_User>();
}
