using DayOff.Application.DTOs.DayOffPolicies;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.DayOffPolicies.Commands
{
    public class UpdateDayOffPolicyCommand : IRequest<bool>
    {
        public UpdateDayOffPolicyDto UpdateDto { get; set; }

        public UpdateDayOffPolicyCommand(UpdateDayOffPolicyDto dto)
        {
            UpdateDto = dto;
        }
    }
}
