using DayOff.Application.DTOs.DayOffRequests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.DayOffRequests.Commands
{
    public class ApproveDayOffCommandValidator : AbstractValidator<ApproveDayOffCommand>
    {
        public ApproveDayOffCommandValidator()
        {
            RuleFor(x => x.ApproveDto)
                .NotNull().WithMessage("Onay bilgileri boş olamaz.")
                .SetValidator(new ApproveDayOffDtoValidator());
        }
    }

}
