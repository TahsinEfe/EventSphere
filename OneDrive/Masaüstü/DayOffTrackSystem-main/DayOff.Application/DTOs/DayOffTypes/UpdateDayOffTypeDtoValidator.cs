using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.DayOffTypes
{
    public class UpdateDayOffTypeDtoValidator : AbstractValidator<UpdateDayOffTypeDto>
    {
        public UpdateDayOffTypeDtoValidator()
        {
            RuleFor(x => x.DyOffId)
                .GreaterThan(0).WithMessage("Geçerli bir izin türü ID'si belirtilmelidir.");

            RuleFor(x => x.DyOffName)
                .NotEmpty().WithMessage("İzin türü adı boş olamaz.")
                .MaximumLength(50).WithMessage("İzin türü adı en fazla 50 karakter olabilir.");

            RuleFor(x => x.AllowedGenderId)
                .NotNull().WithMessage("Cinsiyete özel izin için cinsiyet zorunludur.")
                .When(x => x.IsGenderSpecific);

            RuleFor(x => x.AllowedGenderId)
                .Null().WithMessage("Cinsiyete özel değilse, cinsiyet belirtilmemelidir.")
                .When(x => !x.IsGenderSpecific);
        }
    }
}