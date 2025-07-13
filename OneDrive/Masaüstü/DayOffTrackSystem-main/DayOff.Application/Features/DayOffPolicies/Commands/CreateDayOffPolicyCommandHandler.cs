using DayOff.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.DayOffPolicies.Commands
{
    internal class CreateDayOffPolicyCommandHandler : IRequestHandler<CreateDayOffPolicyCommand, int>
    {
        private readonly IDayOffPolicyService _dayOffPolicyService;

        public CreateDayOffPolicyCommandHandler(IDayOffPolicyService dayOffPolicyService)
        {
            _dayOffPolicyService = dayOffPolicyService;
        }

        public async Task<int> Handle(CreateDayOffPolicyCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CreateDto;

            var entity = new Domain.Entities.DyDayOffPolicy
            {
                DayOffTypeId = dto.DayOffTypeId,
                MinDays = dto.MinDays,
                MaxDays = dto.MaxDays,
                MaxSplitsPerYear = dto.MaxSplitsPerYear,
                MaxConsecutiveDays = dto.MaxConsecutiveDays
            };
            
            return await _dayOffPolicyService.CreateAsync(entity);
        }
    }
}
