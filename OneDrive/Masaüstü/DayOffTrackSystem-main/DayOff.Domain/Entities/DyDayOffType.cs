using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Domain.Entities
{
    public class DyDayOffType
    {
        public int DyOffId { get; set; }
        public string? DyOffName { get; set; }

        public bool? IsGenderSpecific { get; set; }
        public int? AllowedGenderId { get; set; }
        public bool? IsPartialAllowed { get; set; }
    }
}
