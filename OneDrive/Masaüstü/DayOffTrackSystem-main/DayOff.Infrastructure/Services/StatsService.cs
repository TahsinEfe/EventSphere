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
    public class StatsService : IStatsService
    {
        private readonly IStatsRepository _repository;

        public StatsService(IStatsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<VwWeeklyDayOffStat>> GetWeeklyStatsAsync()
        {
            return await _repository.GetWeeklyDayOffStatsAsync();
        }
    }
}
