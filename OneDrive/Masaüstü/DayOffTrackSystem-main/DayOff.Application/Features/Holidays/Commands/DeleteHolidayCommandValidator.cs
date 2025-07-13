using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Holidays.Commands
{
    public class DeleteHolidayCommandValidator : AbstractValidator<DeleteHolidayCommand>
    {
        public DeleteHolidayCommandValidator()
        {
            RuleFor(x => x.HolidayId)
                .GreaterThan(0).WithMessage("Silinecek tatil ID’si geçerli olmalıdır.");
        }
    }
}
