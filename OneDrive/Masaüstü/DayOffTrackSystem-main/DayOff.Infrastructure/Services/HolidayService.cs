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
    public class HolidayService : IHolidayService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HolidayService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DyHoliday>> GetAllAsync()
        {
            return await _unitOfWork.Holidays.GetAllAsync();
        }

        public async Task<DyHoliday?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Holidays.GetByIdAsync(id);
        }

        public async Task<IEnumerable<DyHoliday>> GetByDateAsync(DateTime date)
        {
            var all = await _unitOfWork.Holidays.GetAllAsync();
            return all.Where(h => h.HolidayDate.Date == date.Date).ToList();
        }

        public async Task<int> CreateAsync(DyHoliday holiday)
        {
            await _unitOfWork.Holidays.AddAsync(holiday);
            await _unitOfWork.SaveChangesAsync();
            return holiday.HolidayId;
        }

        public async Task<bool> UpdateAsync(DyHoliday holiday)
        {
            var existing = await _unitOfWork.Holidays.GetByIdAsync(holiday.HolidayId);
            if (existing == null) return false;

            _unitOfWork.Holidays.Update(holiday);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _unitOfWork.Holidays.GetByIdAsync(id);
            if (existing == null) return false;
            
            _unitOfWork.Holidays.Delete(existing);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
