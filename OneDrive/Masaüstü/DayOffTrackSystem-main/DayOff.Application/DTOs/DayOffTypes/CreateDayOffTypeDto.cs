using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.DayOffTypes
{
    public class CreateDayOffTypeDto
    {
        public string DyOffName { get; set; } = null!;
        public bool IsGenderSpecific { get; set; } = false;
        public int? AllowedGenderId { get; set; }
        public bool IsPartialAllowed { get; set; } = true;
    }
}
