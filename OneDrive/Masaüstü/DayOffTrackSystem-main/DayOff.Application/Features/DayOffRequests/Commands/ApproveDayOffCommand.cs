using DayOff.Application.DTOs.DayOffRequests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.DayOffRequests.Commands
{
    public class ApproveDayOffCommand : IRequest<Unit>
    {
        public ApproveDayOffDto ApproveDto { get; set; }

        public ApproveDayOffCommand(ApproveDayOffDto dto)
        {
            ApproveDto = dto;
        }
    }

}
