using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.DayOffRequests.Commands
{
    public class ApproveDayOffRequestCommand : IRequest<bool>
    {
        public int RequestId { get; set; }

        public ApproveDayOffRequestCommand(int requestId)
        {
            RequestId = requestId;
        }
    }
}
