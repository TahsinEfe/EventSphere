using DayOff.Application.DTOs.DayOffBalances;
using DayOff.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.DayOffBalances.Queries
{
    public class GetDayOffBalanceByUserIdQueryHandler : IRequestHandler<GetDayOffBalanceByUserIdQuery, DayOffBalanceDto?>
    {
        private readonly IDayOffBalanceService _balanceService;

        public GetDayOffBalanceByUserIdQueryHandler(IDayOffBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        public async Task<DayOffBalanceDto?> Handle(GetDayOffBalanceByUserIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _balanceService.GetByUserIdAsync(request.UserId);
            if (entity == null) return null;

            return new DayOffBalanceDto
            {
                DayOffBalanceId = entity.DyOffBalanceId,
                UserId = entity.UserId,
                Year = entity.Year,
                TotalDays = entity.TotalDays.HasValue ? Convert.ToDouble(entity.TotalDays.Value) : 0,
                UsedDays = entity.UsedDays.HasValue ? Convert.ToDouble(entity.UsedDays.Value) : 0,
                CarriedOverDays = entity.CarriedOverDays.HasValue ? Convert.ToDouble(entity.CarriedOverDays.Value) : 0
            };
        }
    }
}