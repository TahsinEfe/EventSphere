using System;
using System.Collections.Generic;

namespace DayOff.Persistence.Entities;

public partial class Dy_DayOffHistory
{
    public decimal DyOffHistoryId { get; set; }

    public decimal DayOffRequestId { get; set; }

    public string? ActionType { get; set; }

    public decimal? ChangedByUserId { get; set; }

    public DateTime? ChangeDate { get; set; }

    public string? OldStatus { get; set; }

    public string? NewStatus { get; set; }

    public string? Note { get; set; }

    public virtual Dy_User? ChangedByUser { get; set; }

    public virtual Dy_DayOffRequest DayOffRequest { get; set; } = null!;
}
