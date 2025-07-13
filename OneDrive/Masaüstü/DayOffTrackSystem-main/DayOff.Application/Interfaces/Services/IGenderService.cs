using DayOff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Interfaces.Services
{
    public interface IGenderService
    {
        Task<IEnumerable<DyGender>> GetAllAsync();
        Task<DyGender> GetByIdAsync(int id);
        Task<int> CreateAsync(DyGender gender);
        Task<bool> UpdateAsync(DyGender gender);
        Task<bool> DeleteAsync(int id);
    }
}
