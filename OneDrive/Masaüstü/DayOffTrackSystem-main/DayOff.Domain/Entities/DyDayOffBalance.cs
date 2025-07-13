using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Domain.Entities
{
    public class DyDayOffBalance
    {
        public int DyOffBalanceId { get; set; }
        public int UserId { get; set; }
        public int Year { get; set; }

        public int? TotalDays { get; set; }
        public int? UsedDays { get; set; }
        public int? CarriedOverDays { get; set; }
    }
}
