using DayOff.Application.Interfaces;
using DayOff.Application.Interfaces.Services;
using DayOff.Domain.Entities;

namespace DayOff.Infrastructure.Services
{
    public class DayOffBalanceService : IDayOffBalanceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DayOffBalanceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DyDayOffBalance>> GetAllAsync()
        {
            return await _unitOfWork.DayOffBalances.GetAllAsync();
        }

        public async Task<DyDayOffBalance?> GetByUserAndYearAsync(int userId, int year)
        {
            var all = await _unitOfWork.DayOffBalances.GetAllAsync();
            return all.FirstOrDefault(b => b.UserId == userId && b.Year == year);
        }

        public async Task<int> CreateAsync(DyDayOffBalance balance)
        {
            await _unitOfWork.DayOffBalances.AddAsync(balance);
            await _unitOfWork.SaveChangesAsync();
            return balance.DyOffBalanceId;
        }

        public async Task<bool> UpdateAsync(DyDayOffBalance balance)
        {
            var existing = await _unitOfWork.DayOffBalances.GetByIdAsync(balance.DyOffBalanceId);
            if (existing == null) return false;

            _unitOfWork.DayOffBalances.Update(balance);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var balance = await _unitOfWork.DayOffBalances.GetByIdAsync(id);
            if (balance == null) return false;

            _unitOfWork.DayOffBalances.Delete(balance);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<DyDayOffBalance?> GetByUserIdAsync(int userId)
        {
            var all = await _unitOfWork.DayOffBalances.GetAllAsync();
            return all
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.Year) // en güncel yıl öne alınır
                .FirstOrDefault();
        }
    }
}
