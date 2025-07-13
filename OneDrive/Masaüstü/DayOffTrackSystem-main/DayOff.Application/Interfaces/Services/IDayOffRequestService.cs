using DayOff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   

namespace DayOff.Application.Interfaces.Services
{
    public interface IDayOffRequestService
    {
        Task<IEnumerable<DyDayOffRequest>> GetAllAsync();
        Task<IEnumerable<DyDayOffRequest>> GetByUserIdAsync(int userId);
        Task<DyDayOffRequest?> GetByIdAsync(int id);
        Task<int> CreateAsync(DyDayOffRequest request);
        Task<bool> ApproveAsync(int requestId);
        Task<bool> RejectAsync(int requestId, string rejectReason);

    }
}
