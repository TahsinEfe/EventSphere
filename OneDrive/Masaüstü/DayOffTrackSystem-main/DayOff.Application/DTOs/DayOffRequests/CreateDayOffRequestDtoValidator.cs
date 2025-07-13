using DayOff.Application.DTOs.DayOffRequests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.DayOffRequests
{
    public class CreateDayOffRequestDtoValidator : AbstractValidator<CreateDayOffRequestDto>
    {
        public CreateDayOffRequestDtoValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("Geçerli bir kullanıcı seçilmelidir.");

            RuleFor(x => x.DayOffTypeId)
                .GreaterThan(0).WithMessage("Geçerli bir izin türü seçilmelidir.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Başlangıç tarihi zorunludur.")
                .LessThanOrEqualTo(x => x.EndDate).WithMessage("Başlangıç tarihi, bitiş tarihinden önce olmalıdır.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("Bitiş tarihi zorunludur.");

            RuleFor(x => x.StartTime)
                .MaximumLength(5).When(x => !string.IsNullOrEmpty(x.StartTime));

            RuleFor(x => x.EndTime)
                .MaximumLength(5).When(x => !string.IsNullOrEmpty(x.EndTime));

            RuleFor(x => x.Reason)
                .MaximumLength(255);
        }
    }
}
