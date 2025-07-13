using DayOff.Application.DTOs.DayOffPolicies;
using DayOff.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.DayOffPolicies.Queries
{
    public class GetDayOffPolicyByIdQueryHandler : IRequestHandler<GetDayOffPolicyByIdQuery, DayOffPolicyDto?>
    {
        private readonly IDayOffPolicyService _policyService;

        public GetDayOffPolicyByIdQueryHandler(IDayOffPolicyService policyService)
        {
            _policyService = policyService;
        }

        public async Task<DayOffPolicyDto?> Handle(GetDayOffPolicyByIdQuery request, CancellationToken cancellationToken)
        {
            var policy = await _policyService.GetByIdAsync(request.PolicyId);
            if (policy == null) return null;

            return new DayOffPolicyDto
            {
                DyPolicyId = policy.DyPolicyId,
                DayOffTypeId = policy.DayOffTypeId,
                MinDays = policy.MinDays ?? 1,
                MaxDays = policy.MaxDays ?? 0,
                MaxSplitsPerYear = policy.MaxSplitsPerYear ?? 1,
                MaxConsecutiveDays = policy.MaxConsecutiveDays ?? 0
            };
        }
    }
}