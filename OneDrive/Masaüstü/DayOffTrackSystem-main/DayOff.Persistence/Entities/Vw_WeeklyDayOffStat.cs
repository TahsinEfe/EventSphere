using System;
using System.Collections.Generic;

namespace DayOff.Persistence.Entities;

public partial class Vw_WeeklyDayOffStat
{
    public decimal DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string? WeekNumber { get; set; }

    public string? Year { get; set; }

    public decimal? TotalRequests { get; set; }
}
