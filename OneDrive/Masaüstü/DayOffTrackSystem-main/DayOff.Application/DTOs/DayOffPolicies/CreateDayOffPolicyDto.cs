using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.DayOffPolicies
{
    public class CreateDayOffPolicyDto
    {
        public int DayOffTypeId { get; set; }

        public int MinDays { get; set; } = 1;
        public int? MaxDays { get; set; }
        public int MaxSplitsPerYear { get; set; } = 1;
        public int? MaxConsecutiveDays { get; set; }
    }
}
