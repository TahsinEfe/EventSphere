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
    public class NotificationService : INotificationService
    {

        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            IUnitOfWork _unitOfWork;
        }

        public async Task<IEnumerable<DyNotification>> GetAllAsync()
        {
            return await _unitOfWork.Notifications.GetAllAsync();
        }

        public async Task<IEnumerable<DyNotification>> GetByUserIdAsync(int userId)
        {
            var all = await _unitOfWork.Notifications.GetAllAsync();
            return all.Where(n => n.UserId == userId).ToList();
        }

        public async Task<IEnumerable<DyNotification>> GetUnreadByUserIdAsync(int userId)
        {
            var all = await _unitOfWork.Notifications.GetAllAsync();
            return all.Where(n => n.UserId == userId && n.IsRead == false).ToList();
        }

        public async Task<DyNotification> GetByIdAsync(int id)
        {
            return await _unitOfWork.Notifications.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(DyNotification notification)
        {
            notification.CreatedAt ??= DateTime.Now;
            notification.IsRead = false;

            await _unitOfWork.Notifications.AddAsync(notification);
            await _unitOfWork.SaveChangesAsync();
            return notification.NotificationId;
        }

        public async Task<bool> MarkAsRead(int id)
        {
            var notif = await _unitOfWork.Notifications.GetByIdAsync(id);
            if (notif == null) return false;

            notif.IsRead = true;
            _unitOfWork.Notifications.Update(notif);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var notif = await _unitOfWork.Notifications.GetByIdAsync(id);
            if (notif == null) return false;

            _unitOfWork.Notifications.Delete(notif);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkAsReadAsync(int id)
        {
            var notif = await _unitOfWork.Notifications.GetByIdAsync(id);
            if (notif == null) return false;

            notif.IsRead = true;
            _unitOfWork.Notifications.Update(notif);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}
