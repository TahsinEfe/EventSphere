using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.DayOffPolicies
{
    public class DayOffPolicyDto
    {
        public int DyPolicyId { get; set; }
        public int DayOffTypeId { get; set; }
        public string? DayOffTypeName { get; set; } // JOIN

        public int MinDays { get; set; }
        public int? MaxDays { get; set; }
        public int MaxSplitsPerYear { get; set; }
        public int? MaxConsecutiveDays { get; set; }

    }
}
