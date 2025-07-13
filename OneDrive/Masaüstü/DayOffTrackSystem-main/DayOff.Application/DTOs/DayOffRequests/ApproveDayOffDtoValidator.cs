using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.DayOffRequests
{
    public class ApproveDayOffDtoValidator : AbstractValidator<ApproveDayOffDto>
    {
        public ApproveDayOffDtoValidator()
        {
            RuleFor(x => x.RequestId)
                .GreaterThan(0).WithMessage("Talep ID geçerli olmalıdır.");

            RuleFor(x => x.ApprovedByUserId)
                .GreaterThan(0).WithMessage("Onaylayan kullanıcı geçerli olmalıdır.");
        }
    }
}
