using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.WeeklyDayOffStats
{
    public class WeeklyDayOffStatDto
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;
        public string WeekNumber { get; set; } = null!; //ISO Hafta uyarısı
        public string Year { get; set; } = null!; 
        public int TotalRequests { get; set; }
    }
}
