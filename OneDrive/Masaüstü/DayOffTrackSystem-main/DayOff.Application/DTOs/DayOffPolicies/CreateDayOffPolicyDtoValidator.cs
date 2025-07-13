using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.DayOffPolicies
{
    public class CreateDayOffPolicyDtoValidator : AbstractValidator<CreateDayOffPolicyDto>
    {
        public CreateDayOffPolicyDtoValidator()
        {
            RuleFor(x => x.DayOffTypeId)
           .GreaterThan(0).WithMessage("İzin türü seçilmelidir.");

            RuleFor(x => x.MinDays)
                .GreaterThanOrEqualTo(1).WithMessage("Minimum izin günü 1 veya daha fazla olmalıdır.");

            RuleFor(x => x.MaxDays)
                .GreaterThanOrEqualTo(x => x.MinDays)
                .When(x => x.MaxDays.HasValue)
                .WithMessage("Maksimum izin günü, minimumdan küçük olamaz.");

            RuleFor(x => x.MaxSplitsPerYear)
                .GreaterThanOrEqualTo(1).WithMessage("İzin en az 1 parçaya bölünebilir olmalıdır.");

            RuleFor(x => x.MaxConsecutiveDays)
                .LessThanOrEqualTo(x => x.MaxDays ?? int.MaxValue)
                .When(x => x.MaxConsecutiveDays.HasValue && x.MaxDays.HasValue)
                .WithMessage("Maksimum ardışık gün sayısı, toplam maksimumdan fazla olamaz.");
        }
    }
}
