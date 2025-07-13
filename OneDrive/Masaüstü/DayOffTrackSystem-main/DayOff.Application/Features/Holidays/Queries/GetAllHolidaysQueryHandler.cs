using DayOff.Application.DTOs.Holidays;
using DayOff.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Holidays.Queries
{
    public class GetAllHolidaysQueryHandler : IRequestHandler<GetAllHolidaysQuery, List<HolidayDto>>
    {
        private readonly IHolidayService _holidayService;

        public GetAllHolidaysQueryHandler(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        public async Task<List<HolidayDto>> Handle(GetAllHolidaysQuery request, CancellationToken cancellationToken)
        {
            var holidays = await _holidayService.GetAllAsync();

            return holidays.Select(h => new HolidayDto
            {
                HolidayId = h.HolidayId,
                HolidayDate = h.HolidayDate,
                HolidayName = h.HolidayName,
                HolidayType = h.HolidayType
            }).ToList();
        }
    }
}