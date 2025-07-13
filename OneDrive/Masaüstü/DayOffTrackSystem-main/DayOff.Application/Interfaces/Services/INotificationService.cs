using DayOff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Interfaces.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<DyNotification>> GetAllAsync();
        Task<IEnumerable<DyNotification>> GetByUserIdAsync(int userId);
        Task<IEnumerable<DyNotification>> GetUnreadByUserIdAsync(int userId);
        Task<DyNotification> GetByIdAsync(int id);
        Task<int> CreateAsync(DyNotification notification);
        Task<bool> MarkAsRead(int id);
        Task<bool> DeleteAsync(int id);
        Task<bool> MarkAsReadAsync(int notificationId);

    }
}
