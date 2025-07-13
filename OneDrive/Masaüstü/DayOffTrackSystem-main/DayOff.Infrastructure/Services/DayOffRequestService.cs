using DayOff.Application.Interfaces;
using DayOff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DayOff.Application.Interfaces.Services;


namespace DayOff.Infrastructure.Services
{
    public class DayOffRequestService : IDayOffRequestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DayOffRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DyDayOffRequest>> GetAllAsync()
        {
            return await _unitOfWork.DayOffRequests.GetAllAsync();
        }

        public async Task<IEnumerable<DyDayOffRequest>> GetByUserIdAsync(int userId)
        {
           var all = await _unitOfWork.DayOffRequests.GetAllAsync();
            return all.Where(r => r.UserId == userId).ToList();
        }

        public async Task<DyDayOffRequest?> GetByIdAsync(int id)
        {
            return await _unitOfWork.DayOffRequests.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(DyDayOffRequest request)
        {
            await _unitOfWork.DayOffRequests.AddAsync(request);
            await _unitOfWork.SaveChangesAsync();
            return request.DyOffReqId;
        }

        public async Task<bool> ApproveAsync(int requestId)
        {
            var request = await _unitOfWork.DayOffRequests.GetByIdAsync(requestId);
            if (request == null)
                return false;

            request.Status = "APPROVED";
            request.UpdatedAt = DateTime.Now;

            _unitOfWork.DayOffRequests.Update(request);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> RejectAsync(int requestId, string rejectReason)
        {
            var request = await _unitOfWork.DayOffRequests.GetByIdAsync(requestId);
            if (request == null)
                return false;

            request.Status = "REJECTED";
            request.RejectReason = rejectReason;
            request.UpdatedAt = DateTime.Now;

            _unitOfWork.DayOffRequests.Update(request);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}