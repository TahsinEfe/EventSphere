using DayOff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Interfaces.Services
{
    public interface IStatsService
    {
        Task<IEnumerable<VwWeeklyDayOffStat>> GetWeeklyStatsAsync();
    }
}
