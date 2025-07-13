using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Notifications.Commands
{
    public class MarkNotificationAsReadCommand : IRequest<bool>
    {
        public int NotificationId { get; set; }

        public MarkNotificationAsReadCommand(int notificationId)
        {
            NotificationId = notificationId;
        }
    }
}
