using DayOff.Application.DTOs.DayOffBalances;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.DayOffBalances.Queries
{
    public class GetDayOffBalanceByUserIdQuery : IRequest<DayOffBalanceDto?>
    {
        public int UserId { get; set; }

        public GetDayOffBalanceByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
