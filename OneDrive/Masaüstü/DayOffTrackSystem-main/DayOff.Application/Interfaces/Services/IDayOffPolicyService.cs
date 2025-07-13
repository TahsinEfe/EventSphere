using DayOff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Interfaces.Services
{
    public interface IDayOffPolicyService
    {
        Task<IEnumerable<DyDayOffPolicy>> GetAllAsync();
        Task<IEnumerable<DyDayOffPolicy>> GetByDayOffTypeIdAsync(int dayOffTypeId);
        Task<DyDayOffPolicy?> GetByIdAsync(int id);
        Task<int> CreateAsync(DyDayOffPolicy policy);
        Task<bool> UpdateAsync(DyDayOffPolicy policy);
        Task<bool> DeleteAsync(int id);
    }
}
