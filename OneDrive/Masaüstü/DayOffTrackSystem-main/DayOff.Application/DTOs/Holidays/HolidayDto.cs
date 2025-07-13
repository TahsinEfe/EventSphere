using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.Holidays
{
    public class HolidayDto
    {
        public int HolidayId { get; set; }
        public DateTime HolidayDate { get; set; }
        public string HolidayName { get; set; } = null!;
        public string HolidayType { get; set; } = "RESMİ";
    }
}
