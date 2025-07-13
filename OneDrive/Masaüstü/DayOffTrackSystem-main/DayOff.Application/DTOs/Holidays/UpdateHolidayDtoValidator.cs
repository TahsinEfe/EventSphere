using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.Holidays
{
    public class UpdateHolidayDtoValidator : AbstractValidator<UpdateHolidayDto>
    {
        public UpdateHolidayDtoValidator()
        {
            RuleFor(x => x.HolidayId)
                .GreaterThan(0).WithMessage("Geçerli bir tatil ID'si belirtilmelidir.");

            RuleFor(x => x.HolidayDate)
                .GreaterThanOrEqualTo(DateTime.Today)
                .When(x => x.HolidayDate.HasValue)
                .WithMessage("Tatil tarihi geçmiş olamaz.");

            RuleFor(x => x.HolidayName)
                .MaximumLength(150)
                .When(x => !string.IsNullOrWhiteSpace(x.HolidayName));

            RuleFor(x => x.HolidayType)
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.HolidayType));
        }
    }
}
