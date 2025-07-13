using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.DayOffRequests.Commands
{
    public class CreateDayOffRequestCommandValidator : AbstractValidator<CreateDayOffRequestCommand>
    {
        public CreateDayOffRequestCommandValidator()
        {
            RuleFor(x => x.CreateDto)
                .NotNull().WithMessage("İzin talebi bilgileri boş olamaz.")
                .SetValidator(new CreateDayOffRequestDtoValidator());
        }
    }
}
