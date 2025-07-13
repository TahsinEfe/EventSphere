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
    public class DayOffTypeService : IDayOffTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DayOffTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DyDayOffType>> GetAllAsync()
        {
            return await _unitOfWork.DayOffTypes.GetAllAsync();
        }

        public async Task<DyDayOffType?> GetByIdAsync(int id)
        {
            return await _unitOfWork.DayOffTypes.GetByIdAsync(id);
        }

        public async Task<IEnumerable<DyDayOffType>> GetByAllowedGenderAsync(int genderId)
        {
            var all = await _unitOfWork.DayOffTypes.GetAllAsync();
            return all.Where(x => x.AllowedGenderId == genderId).ToList();
        }

        public async Task<int> CreateAsync(DyDayOffType type)
        {
            await _unitOfWork.DayOffTypes.AddAsync(type);
            await _unitOfWork.SaveChangesAsync();
            return type.DyOffId;
        }

        public async Task<bool> UpdateAsync(DyDayOffType type)
        {
            var existingType = await _unitOfWork.DayOffTypes.GetByIdAsync(type.DyOffId);
            if (existingType == null) return false;

            _unitOfWork.DayOffTypes.Update(type);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var type = await _unitOfWork.DayOffTypes.GetByIdAsync(id);
            if (type == null) return false;

            _unitOfWork.DayOffTypes.Delete(type);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
