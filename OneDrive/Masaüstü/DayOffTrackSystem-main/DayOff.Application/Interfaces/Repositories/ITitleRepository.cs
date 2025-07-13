using DayOff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Interfaces.Repositories
{
    public interface ITitleRepository
    {
        Task<IEnumerable<DyTitle>> GetAllAsync();
        Task<DyTitle?> GetByIdAsync(int id);
        Task AddAsync(DyTitle title);
        void Update(DyTitle title);
        void Delete(DyTitle title);
    }
}
