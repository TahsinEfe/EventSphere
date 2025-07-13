using DayOff.Application.DTOs.DayOffBalances;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.DayOffBalances.Commands
{
    public class UpdateDayOffBalanceCommand : IRequest<bool>
    {
        public UpdateDayOffBalanceDto UpdateDto { get; set; }

        public UpdateDayOffBalanceCommand(UpdateDayOffBalanceDto dto)
        {
            UpdateDto = dto;
        }
    }
}
