using DayOff.Application.DTOs.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Notifications.Commands
{
    public class CreateNotificationCommand : IRequest<int>
    {
        public CreateNotificationDto CreateDto { get; set; }

        public CreateNotificationCommand(CreateNotificationDto dto)
        {
            CreateDto = dto;
        }
    }
}
