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
    public class DayOffPolicyService : IDayOffPolicyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DayOffPolicyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DyDayOffPolicy>> GetAllAsync()
        {
            return await _unitOfWork.DayOffPolicies.GetAllAsync();
        }

        public async Task<IEnumerable<DyDayOffPolicy>> GetByDayOffTypeIdAsync(int dayOffTypeId)
        {
            var all = await _unitOfWork.DayOffPolicies.GetAllAsync();
            return all.Where(p => p.DayOffTypeId == dayOffTypeId).ToList();
        }

        public async Task<DyDayOffPolicy?> GetByIdAsync(int id)
        {
            return await _unitOfWork.DayOffPolicies.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(DyDayOffPolicy policy)
        {
            await _unitOfWork.DayOffPolicies.AddAsync(policy);
            await _unitOfWork.SaveChangesAsync();
            return policy.DyPolicyId;
        }

        public async Task<bool> UpdateAsync(DyDayOffPolicy policy)
        {
            var existing = await _unitOfWork.DayOffPolicies.GetByIdAsync(policy.DyPolicyId);
            if(existing == null) return false;

            _unitOfWork.DayOffPolicies.Update(policy);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var policy = await _unitOfWork.DayOffPolicies.GetByIdAsync(id);
            if (policy == null) return false;

            _unitOfWork.DayOffPolicies.Delete(policy);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
