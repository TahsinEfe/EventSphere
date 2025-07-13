using DayOff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Interfaces.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<DyRole>> GetAllAsync();
        Task<DyRole?> GetByIdAsync(int id);
        Task<int> CreateAsync(DyRole role);
        Task<bool> UpdateAsync(DyRole role);
        Task<bool> DeleteAsync(int id);
    }
}
