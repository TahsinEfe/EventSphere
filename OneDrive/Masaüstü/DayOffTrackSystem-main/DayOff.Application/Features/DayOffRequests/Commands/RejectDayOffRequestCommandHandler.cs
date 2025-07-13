using DayOff.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.DayOffRequests.Commands
{
    public class RejectDayOffRequestCommandHandler : IRequestHandler<RejectDayOffRequestCommand, bool>
    {
        private readonly IDayOffRequestService _dayOffRequestService;

        public RejectDayOffRequestCommandHandler(IDayOffRequestService dayOffRequestService)
        {
            _dayOffRequestService = dayOffRequestService;
        }

        public async Task<bool> Handle(RejectDayOffRequestCommand request, CancellationToken cancellationToken)
        {
            return await _dayOffRequestService.RejectAsync(request.RequestId, request.RejectReason);
        }
    }
}