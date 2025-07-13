using DayOff.Application.Interfaces;
using DayOff.Application.Interfaces.Services;
using DayOff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;


        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DyRole>> GetAllAsync()
        {
            return await _unitOfWork.Roles.GetAllAsync();
        }

        public async Task<DyRole?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Roles.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(DyRole role)
        {
            await _unitOfWork.Roles.AddAsync(role);
            await _unitOfWork.SaveChangesAsync();
            return role.RoleId;
        }


        public async Task<bool> UpdateAsync(DyRole role)
        {
            var existingRole = await _unitOfWork.Roles.GetByIdAsync(role.RoleId);
            if (existingRole == null) return false;

            _unitOfWork.Roles.Update(role);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var role = await _unitOfWork.Roles.GetByIdAsync(id);
            if (role == null) return false;

            _unitOfWork.Roles.Delete(role);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
