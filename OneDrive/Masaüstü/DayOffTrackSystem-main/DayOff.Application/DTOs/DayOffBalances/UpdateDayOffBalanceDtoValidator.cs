using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.DayOffBalances
{
    public class UpdateDayOffBalanceDtoValidator : AbstractValidator<UpdateDayOffBalanceDto>
    {
        public UpdateDayOffBalanceDtoValidator()
        {
            RuleFor(x => x.DayOffBalanceId)
                .GreaterThan(0)
                .WithMessage("İzin bakiyesi kimliği (ID) geçerli olmalıdır.");

            RuleFor(x => x.TotalDays)
                .NotNull().WithMessage("Toplam izin günü boş olamaz.")
                .GreaterThanOrEqualTo(0).WithMessage("Toplam izin günü negatif olamaz.");

            RuleFor(x => x.UsedDays)
                .NotNull().WithMessage("Kullanılmış izin günü boş olamaz.")
                .GreaterThanOrEqualTo(0).WithMessage("Kullanılmış izin günü negatif olamaz.")
                .LessThanOrEqualTo(x => x.TotalDays ?? 0)
                .WithMessage("Kullanılan gün, toplamdan fazla olamaz.");

            RuleFor(x => x.CarriedOverDays)
                .NotNull().WithMessage("Devreden gün boş olamaz.")
                .GreaterThanOrEqualTo(0).WithMessage("Devreden gün negatif olamaz.");
        }
    }
}
