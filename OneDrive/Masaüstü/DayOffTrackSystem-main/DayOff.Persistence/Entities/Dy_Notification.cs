using System;
using System.Collections.Generic;

namespace DayOff.Persistence.Entities;

public partial class Dy_Notification
{
    public decimal NotificationId { get; set; }

    public decimal? UserId { get; set; }

    public string? Title { get; set; }

    public string? Message { get; set; }

    public bool? IsRead { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Dy_User? User { get; set; }
}
