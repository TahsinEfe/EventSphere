using System;
using System.Collections.Generic;

namespace DayOff.Persistence.Entities;

public partial class Dy_User
{
    public decimal UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string TcNo { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Neighborhood { get; set; }

    public string? Street { get; set; }

    public string? Building { get; set; }

    public string? District { get; set; }

    public string? City { get; set; }

    public decimal? GenderId { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public DateTime EmploymentDate { get; set; }

    public bool? IsActive { get; set; }

    public decimal RoleId { get; set; }

    public decimal? DepartmentId { get; set; }

    public decimal? TitleId { get; set; }

    public virtual Dy_Department? Department { get; set; }

    public virtual ICollection<Dy_DayOffBalance> DyDayOffBalances { get; set; } = new List<Dy_DayOffBalance>();

    public virtual ICollection<Dy_DayOffHistory> DyDayOffHistories { get; set; } = new List<Dy_DayOffHistory>();

    public virtual ICollection<Dy_DayOffRequest> DyDayOffRequests { get; set; } = new List<Dy_DayOffRequest>();

    public virtual ICollection<Dy_Notification> DyNotifications { get; set; } = new List<Dy_Notification>();

    public virtual Dy_Gender? Gender { get; set; }

    public virtual Dy_Role Role { get; set; } = null!;

    public virtual Dy_Title? Title { get; set; }
}
