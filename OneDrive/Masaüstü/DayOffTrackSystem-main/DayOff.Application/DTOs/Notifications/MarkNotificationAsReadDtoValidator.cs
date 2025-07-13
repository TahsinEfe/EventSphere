using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.Notifications
{
    public class MarkNotificationAsReadDtoValidator : AbstractValidator<MarkNotificationAsReadDto>
    {
        public MarkNotificationAsReadDtoValidator()
        {
            RuleFor(x => x.NotificationId)
                .GreaterThan(0).WithMessage("Geçerli bir bildirim ID’si girilmelidir.");
        }
    }
}
