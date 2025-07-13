using DayOff.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.DayOffRequests.Commands
{
    internal class ApproveDayOffRequestCommandHandler : IRequestHandler<ApproveDayOffRequestCommand, bool>
    {
        private readonly IDayOffRequestService _dayOffRequestService;
        
        public ApproveDayOffRequestCommandHandler(IDayOffRequestService dayOffRequestService)
        {
            _dayOffRequestService = dayOffRequestService;
        }

        public async Task<bool> Handle(ApproveDayOffRequestCommand request, CancellationToken cancellationToken)
        {
            return await _dayOffRequestService.ApproveAsync(request.RequestId);
        }
    }
}
