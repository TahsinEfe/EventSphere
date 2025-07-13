using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.DayOffBalances
{
    public class DayOffBalanceDto
    {
        public int DayOffBalanceId { get; set; }
        public int UserId { get; set; }
        public string? UserFirstName { get; set; }
        public string? UserLastName { get; set; }

        public int Year { get; set; }
        public double TotalDays { get; set; }
        public double UsedDays { get; set; }
        public double CarriedOverDays { get; set; }

        public double RemainingDays => TotalDays + CarriedOverDays - UsedDays;

    }
}
