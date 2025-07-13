using DayOff.Application.DTOs.DayOffTypes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.DayOffTypes.Commands
{
    public class CreateDayOffTypeCommand : IRequest<int>
    {
        public CreateDayOffTypeDto CreateDto { get; set; }

        public CreateDayOffTypeCommand(CreateDayOffTypeDto dto)
        {
            CreateDto = dto;
        }
    }
}
