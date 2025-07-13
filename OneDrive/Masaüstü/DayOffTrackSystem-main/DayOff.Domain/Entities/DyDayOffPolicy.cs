using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Domain.Entities
{
    public class DyDayOffPolicy
    {
        public int DyPolicyId { get; set; }
        public int DayOffTypeId { get; set; }

        public int? MinDays { get; set; }
        public int? MaxDays { get; set; }
        public int? MaxSplitsPerYear { get; set; }
        public int? MaxConsecutiveDays { get; set; }
    }
}
