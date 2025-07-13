using System;
using System.Collections.Generic;

namespace DayOff.Persistence.Entities;

public partial class Dy_DayOffBalance
{
    public decimal DyOffBalanceId { get; set; }

    public decimal UserId { get; set; }

    public decimal Year { get; set; }

    public decimal? TotalDays { get; set; }

    public decimal? UsedDays { get; set; }

    public decimal? CarriedOverDays { get; set; }

    public virtual Dy_User User { get; set; } = null!;
}
