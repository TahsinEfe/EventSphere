using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.DayOffTypes
{
    public class CreateDayOffTypeDtoValidator : AbstractValidator<CreateDayOffTypeDto>
    {
        public CreateDayOffTypeDtoValidator()
        {
            RuleFor(x => x.DyOffName)
            .NotEmpty().WithMessage("İzin türü adı boş olamaz.")
            .MaximumLength(50).WithMessage("İzin türü adı en fazla 50 karakter olabilir.");

            RuleFor(x => x.AllowedGenderId)
                .NotNull().WithMessage("Cinsiyete özel izin için geçerli bir cinsiyet seçilmelidir.")
                .When(x => x.IsGenderSpecific);

            RuleFor(x => x.AllowedGenderId)
                .Null().WithMessage("Cinsiyete özel değilse, cinsiyet değeri belirtilmemelidir.")
                .When(x => !x.IsGenderSpecific);
        }
    }
}
