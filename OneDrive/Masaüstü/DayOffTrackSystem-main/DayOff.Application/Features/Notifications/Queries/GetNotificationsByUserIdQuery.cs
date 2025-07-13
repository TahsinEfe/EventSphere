using DayOff.Application.DTOs.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Notifications.Queries
{
    public class GetNotificationsByUserIdQuery : IRequest<List<NotificationDto>>
    {
        public int UserId { get; set; }
        public GetNotificationsByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
