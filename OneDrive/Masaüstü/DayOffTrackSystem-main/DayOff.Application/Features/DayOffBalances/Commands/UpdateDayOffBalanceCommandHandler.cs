using DayOff.Application.DTOs.DayOffBalances;
using DayOff.Application.Interfaces.Services;
using DayOff.Domain.Entities;
using MediatR;

namespace DayOff.Application.Features.DayOffBalances.Commands
{
    public class UpdateDayOffBalanceCommandHandler : IRequestHandler<UpdateDayOffBalanceCommand, bool>
    {
        private readonly IDayOffBalanceService _balanceService;

        public UpdateDayOffBalanceCommandHandler(IDayOffBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        public async Task<bool> Handle(UpdateDayOffBalanceCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UpdateDto;

            var entity = new DyDayOffBalance
            {
                DyOffBalanceId = dto.DayOffBalanceId,
                TotalDays = dto.TotalDays.HasValue ? Convert.ToInt32(dto.TotalDays.Value) : 0,
                UsedDays = dto.UsedDays.HasValue ? Convert.ToInt32(dto.UsedDays.Value) : 0,
                CarriedOverDays = dto.CarriedOverDays.HasValue ? Convert.ToInt32(dto.CarriedOverDays.Value) : 0
            };

            return await _balanceService.UpdateAsync(entity);
        }
    }
}
