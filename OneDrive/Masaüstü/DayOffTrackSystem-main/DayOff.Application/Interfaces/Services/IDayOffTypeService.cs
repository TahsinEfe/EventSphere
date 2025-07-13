using DayOff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Interfaces.Services
{
    public interface IDayOffTypeService
    {
        Task<IEnumerable<DyDayOffType>> GetAllAsync();
        Task<DyDayOffType?> GetByIdAsync(int id);
        Task<IEnumerable<DyDayOffType>> GetByAllowedGenderAsync(int genderId);
        Task<int> CreateAsync(DyDayOffType dayOffType);
        Task<bool> UpdateAsync(DyDayOffType dayOffType);
        Task<bool> DeleteAsync(int id);

    }
}
