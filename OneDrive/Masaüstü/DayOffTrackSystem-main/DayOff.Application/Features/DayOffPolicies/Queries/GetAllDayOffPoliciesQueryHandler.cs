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
    public class GetAllDayOffPoliciesQueryHandler : IRequestHandler<GetAllDayOffPoliciesQuery, List<DayOffPolicyDto>>
    {
        private readonly IDayOffPolicyService _policyService;

        public GetAllDayOffPoliciesQueryHandler(IDayOffPolicyService policyService)
        {
            _policyService = policyService;
        }

        public async Task<List<DayOffPolicyDto>> Handle(GetAllDayOffPoliciesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _policyService.GetAllAsync();

            return entities.Select(policy => new DayOffPolicyDto
            {
                DyPolicyId = policy.DyPolicyId,
                DayOffTypeId = policy.DayOffTypeId,
                MinDays = policy.MinDays ?? 1,
                MaxDays = policy.MaxDays, // nullable kalabilir
                MaxSplitsPerYear = policy.MaxSplitsPerYear ?? 1,
                MaxConsecutiveDays = policy.MaxConsecutiveDays
            }).ToList();
        }
    }
}