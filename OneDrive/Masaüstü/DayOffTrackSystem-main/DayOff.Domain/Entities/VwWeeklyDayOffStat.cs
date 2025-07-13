using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Domain.Entities
{
    public class VwWeeklyDayOffStat
{
    public decimal DepartmentId { get; set; }
    public string DepartmentName { get; set; } = null!;
    public string WeekNumber { get; set; } = null!;
    public string Year { get; set; } = null!;
    public decimal? TotalRequests { get; set; }
}

}
