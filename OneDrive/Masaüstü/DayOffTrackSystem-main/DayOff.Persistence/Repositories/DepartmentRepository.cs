using DayOff.Application.Interfaces.Repositories;
using DayOff.Domain.Entities;
using DayOff.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Persistence.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DayOffDbContext _context;

        public DepartmentRepository(DayOffDbContext context)
        {
            _context = context;
        }

        public async Task<List<DyDepartment>> GetAllAsync()
        {
            var departments = await _context.DyDepartments.ToListAsync();

            return departments.Select(d => new DyDepartment
            {
                DepId = (int)d.DepId,
                DepName = d.DepName
            }).ToList();
        }

        public async Task<DyDepartment?> GetByIdAsync(int id)
        {
            var entity = await _context.DyDepartments.FindAsync(id);

            if (entity == null) return null;

            return new DyDepartment
            {
                DepId = (int)entity.DepId,
                DepName = entity.DepName
            };
        }

        public async Task AddAsync(DyDepartment department)
        {
            var entity = new Persistence.Entities.Dy_Department
            {
                DepName = department.DepName,
            };

            await _context.DyDepartments.AddAsync(entity);
            await _context.SaveChangesAsync();

            department.DepId = (int)entity.DepId; // Eğer auto-increment ise
        }

        public async Task UpdateAsync(DyDepartment department)
        {
            var entity = await _context.DyDepartments.FindAsync(department.DepId);
            if (entity == null) return;

            entity.DepName = department.DepName;

            _context.DyDepartments.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.DyDepartments.FindAsync(id);
            if (entity != null)
            {
                _context.DyDepartments.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.DyDepartments.AnyAsync(d => d.DepId == id);
        }
    }
}
