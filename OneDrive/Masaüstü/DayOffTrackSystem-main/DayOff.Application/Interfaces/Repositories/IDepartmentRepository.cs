using DayOff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Interfaces.Repositories
{
    public interface IDepartmentRepository
    {
        Task<DyDepartment?> GetByIdAsync(int id);
        Task<List<DyDepartment>> GetAllAsync();
        Task AddAsync(DyDepartment department);
        Task UpdateAsync(DyDepartment department);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
