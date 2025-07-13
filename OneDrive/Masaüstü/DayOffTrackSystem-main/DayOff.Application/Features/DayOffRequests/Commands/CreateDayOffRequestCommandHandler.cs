using DayOff.Application.Interfaces.Services;
using DayOff.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.DayOffRequests.Commands
{
    public class CreateDayOffRequestCommandHandler : IRequestHandler<CreateDayOffRequestCommand, int>
    {
        private readonly IDayOffRequestService _dayOffRequestService;

        public CreateDayOffRequestCommandHandler(IDayOffRequestService dayOffRequestService)
        {
            _dayOffRequestService = dayOffRequestService;
        }
        public async Task<int> Handle(CreateDayOffRequestCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CreateDto;

            var requestEntity = new DyDayOffRequest
            {
                UserId = dto.UserId,
                DayOffTypeId = dto.DayOffTypeId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Reason = dto.Reason,
                FreeTravelDays = dto.FreeTravelDays,
                Status = "PENDING",
                CreatedAt = DateTime.Now
            };

            return await _dayOffRequestService.CreateAsync(requestEntity);
        }
    }
}