using DayOff.Application.DTOs.WeeklyDayOffStats;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.WeeklyDayOffStats.Queries
{
    public class GetWeeklyDayOffStatsQuery : IRequest<List<WeeklyDayOffStatDto>>
    {
    }
}
