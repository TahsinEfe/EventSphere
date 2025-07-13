using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.Holidays
{
    public class CreateHolidayDtoValidator : AbstractValidator<CreateHolidayDto>
    {
        public CreateHolidayDtoValidator()
        {
            RuleFor(x => x.HolidayDate)
                .NotEmpty().WithMessage("Tatil tarihi zorunludur.")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Tatil tarihi geçmiş olamaz.");

            RuleFor(x => x.HolidayName)
                .NotEmpty().WithMessage("Tatil adı boş olamaz.")
                .MaximumLength(150);

            RuleFor(x => x.HolidayType)
                .NotEmpty().WithMessage("Tatil türü boş olamaz.")
                .MaximumLength(50);
        }
    }
}
