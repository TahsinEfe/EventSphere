using System;
using System.Collections.Generic;

namespace DayOff.Persistence.Entities;

public partial class Dy_DayOffRequest
{
    public decimal DyOffReqId { get; set; }

    public decimal UserId { get; set; }

    public decimal DayOffTypeId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? StartTime { get; set; }

    public string? EndTime { get; set; }

    public decimal? DurationDays { get; set; }

    public decimal? DurationHours { get; set; }

    public string? Reason { get; set; }

    public decimal? FreeTravelDays { get; set; }

    public string? Status { get; set; }

    public string? RejectReason { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Dy_DayOffType DayOffType { get; set; } = null!;

    public virtual ICollection<Dy_DayOffHistory> DyDayOffHistories { get; set; } = new List<Dy_DayOffHistory>();

    public virtual Dy_User User { get; set; } = null!;
}
