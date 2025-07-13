using System;
using System.Collections.Generic;

namespace DayOff.Persistence.Entities;

public partial class Dy_DayOffPolicy
{
    public decimal DyPolicyId { get; set; }

    public decimal DayOffTypeId { get; set; }

    public decimal? MinDays { get; set; }

    public decimal? MaxDays { get; set; }

    public decimal? MaxSplitsPerYear { get; set; }

    public decimal? MaxConsecutiveDays { get; set; }

    public virtual Dy_DayOffType DayOffType { get; set; } = null!;
}
