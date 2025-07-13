using DayOff.Application.Interfaces.Repositories;
using DayOff.Domain.Entities;
using DayOff.Persistence.Context;
using DayOff.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Persistence.Repositories
{
    public class TitleRepository : ITitleRepository
    {
        private readonly DayOffDbContext _context;

        public TitleRepository(DayOffDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DyTitle>> GetAllAsync()
        {
            var titles = await _context.DyTitles.ToListAsync(); 
            return titles.Select(t => new DyTitle
            {
                TitleId = (int)t.TitleId,
                TitleName = t.TitleName
            });
        }

        public async Task<DyTitle?> GetByIdAsync(int id)
        {
            var entity = await _context.DyTitles.FindAsync((decimal)id);
            if (entity == null) return null;

            return new DyTitle
            {
                TitleId = (int)entity.TitleId,
                TitleName = entity.TitleName
            };
        }

        public async Task AddAsync(DyTitle title)
        {
            var entity = new Dy_Title
            {
                TitleName = title.TitleName
            };

            await _context.DyTitles.AddAsync(entity);
            title.TitleId = (int)entity.TitleId; 
        }

        public void Update(DyTitle title)
        {
            var entity = new Dy_Title
            {
                TitleId = title.TitleId,
                TitleName = title.TitleName
            };

            _context.DyTitles.Update(entity);
        }

        public void Delete(DyTitle title)
        {
            var entity = _context.DyTitles.Find((decimal)title.TitleId);
            if (entity != null)
            {
                _context.DyTitles.Remove(entity);
            }
        }


    }
}
