using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.DayOffRequests
{
    internal class UpdateDayOffRequestDto
    {
        public int DayOffReqId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string? StartTime { get; set; }
        public string? EndTime { get; set; }

        public string? Reason { get; set; }
        public int? FreeTravelDays { get; set; }

        public string? Status { get; set; } 
        public string? RejectReason { get; set; }
    }
}
