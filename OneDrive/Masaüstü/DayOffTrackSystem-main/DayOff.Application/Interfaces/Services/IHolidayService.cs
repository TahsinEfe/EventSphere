using DayOff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Interfaces.Services
{
    public interface IHolidayService 
    {
        Task<IEnumerable<DyHoliday>> GetAllAsync();
        Task<DyHoliday?> GetByIdAsync(int id);
        Task<IEnumerable<DyHoliday>> GetByDateAsync(DateTime date);
        Task<int> CreateAsync(DyHoliday holiday);
        Task<bool> UpdateAsync(DyHoliday holiday);
        Task<bool> DeleteAsync(int id);
    }
}
