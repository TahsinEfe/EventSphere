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
    public class GenderService : IGenderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DyGender>> GetAllAsync()
        {
            return await _unitOfWork.Genders.GetAllAsync();
        }

        public async Task<DyGender?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Genders.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(DyGender gender)
        {
            await _unitOfWork.Genders.AddAsync(gender);
            await _unitOfWork.SaveChangesAsync();
            return gender.GenderId;
        }

        public async Task<bool> UpdateAsync(DyGender gender)
        {
            var existing = await _unitOfWork.Genders.GetByIdAsync(gender.GenderId);
            if (existing == null) return false;

            _unitOfWork.Genders.Update(gender);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var gender = await _unitOfWork.Genders.GetByIdAsync(id);
            if (gender == null) return false;

            _unitOfWork.Genders.Delete(gender);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }

}
