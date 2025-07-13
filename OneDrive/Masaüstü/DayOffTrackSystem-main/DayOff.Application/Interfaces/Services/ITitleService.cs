using DayOff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Interfaces.Services
{
    public interface ITitleService
    {
        Task<IEnumerable<DyTitle>> GetAllAsync();
        Task<DyTitle?> GetByIdAsync(int id);
        Task<int> CreateAsync(DyTitle title);
        Task<bool> UpdateAsync(DyTitle title);
        Task<bool> DeleteAsync(int id);
    }
}
