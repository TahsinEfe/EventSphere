using DayOff.Application.Interfaces.Services;
using DayOff.Domain.Entities;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.DayOffPolicies.Commands
{
    public class UpdateDayOffPolicyCommandHandler : IRequestHandler<UpdateDayOffPolicyCommand, bool>
    {
        private readonly IDayOffPolicyService _policyService;

        public UpdateDayOffPolicyCommandHandler(IDayOffPolicyService policyService)
        {
            _policyService = policyService;
        }

        public async Task<bool> Handle(UpdateDayOffPolicyCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UpdateDto;

            var entity = new DyDayOffPolicy
            {
                DyPolicyId = dto.DyPolicyId,
                MinDays = dto.MinDays ?? 1,
                MaxDays = dto.MaxDays,
                MaxSplitsPerYear = dto.MaxSplitsPerYear ?? 1,
                MaxConsecutiveDays = dto.MaxConsecutiveDays
            };

            return await _policyService.UpdateAsync(entity);
        }
    }
}