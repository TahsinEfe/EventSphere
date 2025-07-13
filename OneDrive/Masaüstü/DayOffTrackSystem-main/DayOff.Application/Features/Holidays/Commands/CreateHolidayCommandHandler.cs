using DayOff.Application.Interfaces.Services;
using DayOff.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Holidays.Commands
{
    public class CreateHolidayCommandHandler :IRequestHandler<CreateHolidayCommand, int>
    {
        private readonly IHolidayService _holidayService;

        public CreateHolidayCommandHandler(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        public async Task<int> Handle(CreateHolidayCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CreateDto;

            var entity = new DyHoliday
            {
                HolidayDate = dto.HolidayDate,
                HolidayName = dto.HolidayName,
                HolidayType = dto.HolidayType ?? "RESMI"
            };

            return await _holidayService.CreateAsync(entity);
        }
    }
}