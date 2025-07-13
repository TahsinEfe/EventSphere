using DayOff.Application.Interfaces.Repositories;
using DayOff.Domain.Entities;
using DayOff.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DayOff.Persistence.Repositories
{
    public class DayOffTypeRepository : IDayOffTypeRepository
    {
        private readonly DayOffDbContext _context;

        public DayOffTypeRepository(DayOffDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DyDayOffType>> GetAllAsync()
        {
            var entities = await _context.DyDayOffTypes.ToListAsync();

            Console.WriteLine("Gelen kayıt sayısı: " + entities.Count); // DEBUG

            return entities.Select(e => new DyDayOffType
            {
                DyOffId = (int)e.DyOffId,
                DyOffName = e.DyOffName,
                IsGenderSpecific = e.IsGenderSpecific,
                AllowedGenderId = (int?)e.AllowedGenderId,
                IsPartialAllowed = e.IsPartialAllowed
            });
        }
        public async Task<IEnumerable<DyDayOffType>> GetByAllowedGenderAsync(int genderId)
        {
            var entities = await _context.DyDayOffTypes
                .Where(x =>
                    x.IsGenderSpecific == false ||
                    (x.IsGenderSpecific == true && x.AllowedGenderId == (decimal)genderId)
                )
                .ToListAsync();

            return entities.Select(e => new DyDayOffType
            {
                DyOffId = (int)e.DyOffId,
                DyOffName = e.DyOffName,
                IsGenderSpecific = e.IsGenderSpecific,
                AllowedGenderId = (int?)e.AllowedGenderId,
                IsPartialAllowed = e.IsPartialAllowed
            });
        }

        public async Task<DyDayOffType?> GetByIdAsync(int id)
        {
            var entity = await _context.DyDayOffTypes.FindAsync((decimal)id);
            if (entity == null) return null;

            return new DyDayOffType
            {
                DyOffId = (int)entity.DyOffId,
                DyOffName = entity.DyOffName,
                IsGenderSpecific = entity.IsGenderSpecific,
                AllowedGenderId = (int?)entity.AllowedGenderId,
                IsPartialAllowed = entity.IsPartialAllowed
            };
        }

        public async Task AddAsync(DyDayOffType type)
        {
            var entity = new Entities.Dy_DayOffType
            {
                DyOffId = type.DyOffId,
                DyOffName = type.DyOffName,
                IsGenderSpecific = type.IsGenderSpecific,
                AllowedGenderId = type.AllowedGenderId,
                IsPartialAllowed = type.IsPartialAllowed
            };

            await _context.DyDayOffTypes.AddAsync(entity);
        }

        public void Update(DyDayOffType type)
        {
            var entity = new Entities.Dy_DayOffType
            {
                DyOffId = type.DyOffId,
                DyOffName = type.DyOffName,
                IsGenderSpecific = type.IsGenderSpecific,
                AllowedGenderId = type.AllowedGenderId,
                IsPartialAllowed = type.IsPartialAllowed
            };

            _context.DyDayOffTypes.Update(entity);
        }

        public void Delete(DyDayOffType type)
        {
            var entity = new Entities.Dy_DayOffType
            {
                DyOffId = type.DyOffId
            };

            _context.DyDayOffTypes.Remove(entity);
        }
    }
}
