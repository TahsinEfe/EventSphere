using DayOff.Application.Interfaces;
using DayOff.Application.Interfaces.Repositories;
using DayOff.Application.Interfaces.Services;
using DayOff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Infrastructure.Services
{
    public class WeeklyDayOffStatService : IWeeklyDayOffStatService
    {
        private readonly IStatsRepository _statsRepository;

        public WeeklyDayOffStatService(IStatsRepository statsRepository)
        {
            _statsRepository = statsRepository;
        }

        public async Task<IEnumerable<VwWeeklyDayOffStat>> GetAllAsync()
        {
            return await _statsRepository.GetWeeklyDayOffStatsAsync();
        }

        public async Task<IEnumerable<VwWeeklyDayOffStat>> GetByYearAsync(string year)
        {
            var allStats = await _statsRepository.GetWeeklyDayOffStatsAsync();
            return allStats.Where(s => s.Year == year);
        }

        public async Task<IEnumerable<VwWeeklyDayOffStat>> GetByDepartmentIdAsync(int departmentId)
        {
            var allStats = await _statsRepository.GetWeeklyDayOffStatsAsync();
            return allStats.Where(s => s.DepartmentId == departmentId);
        }
    }
}
