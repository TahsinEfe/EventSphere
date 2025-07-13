using DayOff.Application.Interfaces.Repositories;
using DayOff.Domain.Entities;
using DayOff.Persistence.Context;
using DayOff.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace DayOff.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DayOffDbContext _context;

        public UserRepository(DayOffDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(DyUser user)
        {
            var entity = new Dy_User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                TcNo = user.TcNo,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Neighborhood = user.Neighborhood,
                Street = user.Street,
                Building = user.Building,
                District = user.District,
                City = user.City,
                GenderId = user.GenderId,
                DateOfBirth = user.DateOfBirth,
                EmploymentDate = user.EmploymentDate,
                IsActive = user.IsActive,
                RoleId = user.RoleId,
                DepartmentId = user.DepartmentId,
                TitleId = user.TitleId
            };

            await _context.DyUsers.AddAsync(entity);
            await _context.SaveChangesAsync();

            return (int)entity.UserId;
        }

        public async Task<DyUser?> GetByIdAsync(int userId)
        {
            var entity = await _context.DyUsers
                .Include(u => u.Gender)
                .Include(u => u.Role)
                .Include(u => u.Department)
                .Include(u => u.Title)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (entity == null) return null;

            return new DyUser
            {
                UserId = (int)entity.UserId,
                FirstName = entity.FirstName ?? "",
                LastName = entity.LastName ?? "",
                TcNo = entity.TcNo,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber ?? "",
                Neighborhood = entity.Neighborhood ?? "",
                Street = entity.Street ?? "",
                Building = entity.Building ?? "",
                District = entity.District ?? "",
                City = entity.City ?? "",
                GenderId = entity.GenderId != null ? (int?)entity.GenderId : null,
                DateOfBirth = entity.DateOfBirth,
                EmploymentDate = entity.EmploymentDate,
                IsActive = entity.IsActive ?? true,
                RoleId = (int)entity.RoleId,
                DepartmentId = entity.DepartmentId != null ? (int?)entity.DepartmentId : null,
                TitleId = entity.TitleId != null ? (int?)entity.TitleId : null
            };
        }

        public async Task<IEnumerable<DyUser>> GetAllAsync()
        {
            var users = await _context.DyUsers.ToListAsync();

            return users.Select(u => new DyUser
            {
                UserId = (int)u.UserId,
                FirstName = u.FirstName ?? "",
                LastName = u.LastName ?? "",
                TcNo = u.TcNo,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber ?? "",
                Neighborhood = u.Neighborhood ?? "",
                Street = u.Street ?? "",
                Building = u.Building ?? "",
                District = u.District ?? "",
                City = u.City ?? "",
                GenderId = u.GenderId != null ? (int?)u.GenderId : null,
                DateOfBirth = u.DateOfBirth,
                EmploymentDate = u.EmploymentDate,
                IsActive = u.IsActive ?? true,
                RoleId = (int)u.RoleId,
                DepartmentId = u.DepartmentId != null ? (int?)u.DepartmentId : null,
                TitleId = u.TitleId != null ? (int?)u.TitleId : null
            });
        }

        public async Task AddAsync(DyUser user)
        {
            var entity = new Dy_User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                TcNo = user.TcNo,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Neighborhood = user.Neighborhood,
                Street = user.Street,
                Building = user.Building,
                District = user.District,
                City = user.City,
                GenderId = user.GenderId,
                DateOfBirth = user.DateOfBirth,
                EmploymentDate = user.EmploymentDate,
                IsActive = user.IsActive,
                RoleId = user.RoleId,
                DepartmentId = user.DepartmentId,
                TitleId = user.TitleId
            };

            await _context.DyUsers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(DyUser user)
        {
            var entity = await _context.DyUsers.FindAsync((decimal)user.UserId);
            if (entity == null) return false;

            entity.FirstName = user.FirstName;
            entity.LastName = user.LastName;
            entity.TcNo = user.TcNo;
            entity.Email = user.Email;
            entity.PhoneNumber = user.PhoneNumber;
            entity.Neighborhood = user.Neighborhood;
            entity.Street = user.Street;
            entity.Building = user.Building;
            entity.District = user.District;
            entity.City = user.City;
            entity.GenderId = user.GenderId;
            entity.DateOfBirth = user.DateOfBirth;
            entity.EmploymentDate = user.EmploymentDate;
            entity.IsActive = user.IsActive;
            entity.RoleId = user.RoleId;
            entity.DepartmentId = user.DepartmentId;
            entity.TitleId = user.TitleId;

            _context.DyUsers.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int userId)
        {
            var entity = await _context.DyUsers.FindAsync((decimal)userId);
            if (entity == null) return false;

            _context.DyUsers.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int userId)
        {
            return await _context.DyUsers.AnyAsync(u => u.UserId == userId);
        }
    }
}
