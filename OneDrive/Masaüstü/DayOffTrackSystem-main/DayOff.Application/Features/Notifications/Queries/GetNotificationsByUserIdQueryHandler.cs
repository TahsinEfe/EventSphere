using DayOff.Application.DTOs.Notifications;
using DayOff.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Notifications.Queries
{
    public class GetNotificationsByUserIdQueryHandler : IRequestHandler<GetNotificationsByUserIdQuery, List<NotificationDto>>
    {
        private readonly INotificationService _notificationService;

        public GetNotificationsByUserIdQueryHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<List<NotificationDto>> Handle(GetNotificationsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var notifications = await _notificationService.GetByUserIdAsync(request.UserId);

            return notifications.Select(n => new NotificationDto
            {
                NotificationId = n.NotificationId,
                UserId = n.UserId,
                Title = n.Title,
                Message = n.Message,
                IsRead = n.IsRead ?? false,
                CreatedAt = n.CreatedAt ?? DateTime.MinValue,
            }).ToList();
        }
    }
}
