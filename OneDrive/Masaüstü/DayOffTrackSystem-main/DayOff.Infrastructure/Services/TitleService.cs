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
    public class TitleService : ITitleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TitleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DyTitle>> GetAllAsync()
        {
            return await _unitOfWork.Titles.GetAllAsync();
        }

        public async Task<DyTitle?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Titles.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(DyTitle title)
        {
            await _unitOfWork.Titles.AddAsync(title);
            await _unitOfWork.SaveChangesAsync();
            return title.TitleId;
        }

        public async Task<bool> UpdateAsync(DyTitle title)
        {
            var existing = await _unitOfWork.Titles.GetByIdAsync(title.TitleId);
            if (existing == null) return false;

            _unitOfWork.Titles.Update(title);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var title = await _unitOfWork.Titles.GetByIdAsync(id);
            if (title == null) return false;

            _unitOfWork.Titles.Delete(title);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
