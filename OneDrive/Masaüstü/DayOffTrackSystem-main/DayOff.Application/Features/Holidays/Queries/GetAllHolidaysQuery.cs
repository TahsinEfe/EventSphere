using DayOff.Application.DTOs.Holidays;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Holidays.Queries
{
    public class GetAllHolidaysQuery : IRequest<List<HolidayDto>>
    {
    }
}
