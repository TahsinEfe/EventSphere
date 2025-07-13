using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Domain.Entities
{
    public class DyDayOffHistory
    {
        public int DyOffHistoryId { get; set; }
        public int DayOffRequestId { get; set; }

        public string? ActionType { get; set; }
        public int? ChangedByUserId { get; set; }
        public DateTime? ChangeDate { get; set; }
        public string? OldStatus { get; set; }
        public string? NewStatus { get; set; }
        public string? Note { get; set; }
    }
}
