using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.DayOffRequests
{
    public class ApproveDayOffDto
    {
        public int RequestId { get; set; }
        public int ApprovedByUserId { get; set; }
    }

}
