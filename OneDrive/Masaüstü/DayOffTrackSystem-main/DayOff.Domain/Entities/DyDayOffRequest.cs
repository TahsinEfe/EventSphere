using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Domain.Entities
{
    public class DyDayOffRequest
    {
        public int DyOffReqId { get; set; }
        public int UserId { get; set; }
        public int DayOffTypeId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string? StartTime { get; set; }
        public string? EndTime { get; set; }

        public int? DurationDays { get; set; }
        public int? DurationHours { get; set; }

        public string? Reason { get; set; }
        public int? FreeTravelDays { get; set; }

        public string? Status { get; set; }
        public string? RejectReason { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
