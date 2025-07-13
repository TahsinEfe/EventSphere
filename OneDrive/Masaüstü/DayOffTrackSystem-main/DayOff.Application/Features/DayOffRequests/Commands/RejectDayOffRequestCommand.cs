using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.DayOffRequests.Commands
{
    public class RejectDayOffRequestCommand : IRequest<bool>
    {
        public int RequestId { get; set; }
        public string RejectReason { get; set; }

        public RejectDayOffRequestCommand(int requestId, string rejectReason)
        {
            RequestId = requestId;
            RejectReason = rejectReason;
        }
    }
}