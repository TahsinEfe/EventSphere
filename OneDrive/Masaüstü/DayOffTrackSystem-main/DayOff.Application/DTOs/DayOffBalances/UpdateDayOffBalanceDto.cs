using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.DayOffBalances
{
    public class UpdateDayOffBalanceDto
    {
        public int DayOffBalanceId { get; set; }
        public double? TotalDays { get; set; }
        public double? UsedDays { get; set; }
        public double? CarriedOverDays { get; set; }
    }
}
