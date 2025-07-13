using DayOff.Application.DTOs.DayOffPolicies;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.DayOffPolicies.Commands
{
    internal class CreateDayOffPolicyCommand : IRequest<int>
    {
        public CreateDayOffPolicyDto CreateDto { get; set; }

        public CreateDayOffPolicyCommand(CreateDayOffPolicyDto dto)
        {
            CreateDto = dto;
        }
    }
}
