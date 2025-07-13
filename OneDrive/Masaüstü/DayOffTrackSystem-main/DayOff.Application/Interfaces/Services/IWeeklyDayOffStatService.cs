using DayOff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Interfaces.Services
{
    public interface IWeeklyDayOffStatService
    {
        Task<IEnumerable<VwWeeklyDayOffStat>> GetAllAsync();
        Task<IEnumerable<VwWeeklyDayOffStat>> GetByDepartmentIdAsync(int departmentId);
        Task<IEnumerable<VwWeeklyDayOffStat>> GetByYearAsync(string year);
    }
}
