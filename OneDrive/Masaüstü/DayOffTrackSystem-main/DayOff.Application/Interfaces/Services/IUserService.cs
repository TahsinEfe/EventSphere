using DayOff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<DyUser>> GetAllAsync();
        Task<DyUser?> GetByIdAsync(int id);
        Task<int> CreateAsync(DyUser user);
        Task<bool> UpdateAsync(DyUser user);
        Task<bool> DeleteAsync(int id);
    }
}
