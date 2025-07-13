using DayOff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Interfaces.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DyDepartment>> GetAllAsync();
        Task<DyDepartment?> GetByIdAsync(int id);
        Task<int> CreateAsync(DyDepartment department);
        Task<bool> UpdateAsync(DyDepartment department);
        Task<bool> DeleteAsync(int id);
    }
}
