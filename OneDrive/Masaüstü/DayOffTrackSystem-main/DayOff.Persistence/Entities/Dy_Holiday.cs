using System;
using System.Collections.Generic;

namespace DayOff.Persistence.Entities;

public partial class Dy_Holiday
{
    public decimal HolidayId { get; set; }

    public DateTime HolidayDate { get; set; }

    public string HolidayName { get; set; } = null!;

    public string? HolidayType { get; set; }
}
