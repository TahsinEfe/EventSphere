using DayOff.Application.DTOs.DayOffPolicies;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.DayOffPolicies.Queries
{
    public class GetDayOffPolicyByIdQuery : IRequest<DayOffPolicyDto>
    {
        public int PolicyId { get; set; }

        public GetDayOffPolicyByIdQuery(int policyId)
        {
            PolicyId = policyId;
        }
    }
}
