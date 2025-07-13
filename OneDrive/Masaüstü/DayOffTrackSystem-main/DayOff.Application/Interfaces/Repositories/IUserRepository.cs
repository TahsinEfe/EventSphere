using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DayOff.Domain.Entities;




namespace DayOff.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<DyUser>> GetAllAsync();
        Task<DyUser?> GetByIdAsync(int id);
        Task<int> CreateAsync(DyUser user);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(DyUser user);
    }
}
