using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.DayOffRequests
{
    internal class DayOffRequestDetailDto
    {
        public DayOffRequestDto Request { get; set; } = null!;
        public string? DepartmentName { get; set; }
        public string? TitleName { get; set; }
        public string? GenderName { get; set; }
        public int UsedDaysThisYear { get; set; }
        public int RemainingBalance { get; set; }
        
    }
}
