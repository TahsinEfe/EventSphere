using DayOff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Interfaces.Services
{
    public interface IDayOffBalanceService
    {
        Task<IEnumerable<DyDayOffBalance>> GetAllAsync();
        Task<DyDayOffBalance?> GetByUserAndYearAsync(int userId, int year);
        Task<int> CreateAsync(DyDayOffBalance balance);
        Task<bool> UpdateAsync(DyDayOffBalance balance);
        Task<bool> DeleteAsync(int id);
        Task<DyDayOffBalance?> GetByUserIdAsync(int userId);

    }
}
