using DayOff.Application.Interfaces.Repositories;
using DayOff.Domain.Entities;
using DayOff.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Persistence.Repositories
{
    public class StatsRepository : IStatsRepository
    {
        private readonly DayOffDbContext _context;

        public StatsRepository(DayOffDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VwWeeklyDayOffStat>> GetWeeklyDayOffStatsAsync()
        {
            var data = await _context.VwWeeklyDayOffStats.ToListAsync(); 

            return data.Select(x => new VwWeeklyDayOffStat 
            {
                DepartmentId = (int)x.DepartmentId,
                DepartmentName = x.DepartmentName,
                WeekNumber = x.WeekNumber,
                Year = x.Year,
                TotalRequests = x.TotalRequests
            });
        }

    }
}
