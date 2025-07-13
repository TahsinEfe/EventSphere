using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.Users
{
    public class UserDetailsDto
    {
        public UserDto User { get; set; }
        public List<string> Permissions { get; set; } = new();
        public int RemainingAnnualDayOff { get; set; }
        public int UsedDayOff { get; set; }
    }
}
