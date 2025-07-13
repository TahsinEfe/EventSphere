using DayOff.Application.DTOs.WeeklyDayOffStats;
using DayOff.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.WeeklyDayOffStats.Queries
{
    public class GetWeeklyDayOffStatsQueryHandler : IRequestHandler<GetWeeklyDayOffStatsQuery, List<WeeklyDayOffStatDto>>
    {
        private readonly IWeeklyDayOffStatService _statService;

        public GetWeeklyDayOffStatsQueryHandler(IWeeklyDayOffStatService statService)
        {
            _statService = statService;
        }

        public async Task<List<WeeklyDayOffStatDto>> Handle(GetWeeklyDayOffStatsQuery request, CancellationToken cancellationToken)
        {
            var stats = await _statService.GetAllAsync();
        
            return stats.Select(s => new WeeklyDayOffStatDto
            {
                DepartmentId = (int)s.DepartmentId,
                DepartmentName = s.DepartmentName,
                WeekNumber = s.WeekNumber,
                Year = s.Year,
                TotalRequests = (int)s.TotalRequests,
            }).ToList();
        }
    }
}
