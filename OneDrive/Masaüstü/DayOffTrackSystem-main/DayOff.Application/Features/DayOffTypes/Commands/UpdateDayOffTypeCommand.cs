using DayOff.Application.DTOs.DayOffBalances;
using DayOff.Application.DTOs.DayOffTypes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.DayOffTypes.Commands
{
    public class UpdateDayOffTypeCommand : IRequest<bool>
    {
        public UpdateDayOffTypeDto UpdateDto { get; set; }

        public UpdateDayOffTypeCommand(UpdateDayOffTypeDto dto)
        {
            UpdateDto = dto;
        }
    }
}
