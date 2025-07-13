using DayOff.Application.DTOs.DayOffRequests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.DayOffRequests.Commands
{
    public class CreateDayOffRequestCommand : IRequest<int>
    {
        public CreateDayOffRequestDto CreateDto { get; set; }

        public CreateDayOffRequestCommand(CreateDayOffRequestDto dto)
        {
            CreateDto = dto;
        }
    }
}
