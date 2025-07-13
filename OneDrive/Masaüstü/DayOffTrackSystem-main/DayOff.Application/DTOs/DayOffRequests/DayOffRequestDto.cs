using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.DayOffRequests
{
    public class DayOffRequestDto
    {
        public int DyOffReqId { get; set; }
        public int UserId { get; set; }
        public string? UserFirstName { get; set; }
        public string? UserLastName { get; set; }

        public int DayOffTypeId { get; set; }
        public string? DayOffTypeName { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string? StartTime { get; set; }
        public string? EndTime { get; set; }

        public double? DurationDays { get; set; }
        public double? DurationHours { get; set; }

        public string? Reason { get; set; }
        public int FreeTravelDays { get; set; }

        public string Status { get; set; } = "PENDING";
        public string? RejectReason { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
