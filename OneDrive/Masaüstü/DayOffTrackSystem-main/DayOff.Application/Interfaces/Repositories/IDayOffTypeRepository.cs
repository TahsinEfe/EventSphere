using DayOff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Interfaces.Repositories
{
    public interface IDayOffTypeRepository
    {
        Task<IEnumerable<DyDayOffType>> GetByAllowedGenderAsync(int genderId);
        Task<IEnumerable<DyDayOffType>> GetAllAsync();
        Task<DyDayOffType?> GetByIdAsync(int id);
        Task AddAsync(DyDayOffType type);
        void Update(DyDayOffType type);
        void Delete(DyDayOffType type);

    }
}
