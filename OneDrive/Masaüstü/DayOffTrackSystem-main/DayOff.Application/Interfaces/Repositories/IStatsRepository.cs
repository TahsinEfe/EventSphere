using DayOff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Interfaces.Repositories
{
    public interface IStatsRepository
    {
        Task<IEnumerable<VwWeeklyDayOffStat>> GetWeeklyDayOffStatsAsync();
    }
}
