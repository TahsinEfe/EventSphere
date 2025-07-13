using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.DayOffPolicies
{
    public class UpdateDayOffPolicyDtoValidator : AbstractValidator<UpdateDayOffPolicyDto>
    {
        public UpdateDayOffPolicyDtoValidator()
        {
            RuleFor(x => x.DyPolicyId)
                .GreaterThan(0).WithMessage("Politika ID’si geçerli olmalıdır.");

            RuleFor(x => x.MinDays)
                .GreaterThanOrEqualTo(1)
                .When(x => x.MinDays.HasValue)
                .WithMessage("Minimum izin günü en az 1 olmalıdır.");

            RuleFor(x => x.MaxDays)
                .GreaterThanOrEqualTo(x => x.MinDays ?? 1)
                .When(x => x.MaxDays.HasValue)
                .WithMessage("Maksimum izin günü, minimumdan küçük olamaz.");

            RuleFor(x => x.MaxSplitsPerYear)
                .GreaterThanOrEqualTo(1)
                .When(x => x.MaxSplitsPerYear.HasValue)
                .WithMessage("İzin en az 1 parçaya bölünebilir olmalıdır.");

            RuleFor(x => x.MaxConsecutiveDays)
                .LessThanOrEqualTo(x => x.MaxDays ?? int.MaxValue)
                .When(x => x.MaxConsecutiveDays.HasValue && x.MaxDays.HasValue)
                .WithMessage("Ardışık gün sayısı toplamdan büyük olamaz.");
        }
    }
}
