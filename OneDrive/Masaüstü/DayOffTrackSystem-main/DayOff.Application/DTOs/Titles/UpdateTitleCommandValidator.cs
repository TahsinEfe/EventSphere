using DayOff.Application.Features.Titles.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.Titles
{
    public class UpdateTitleCommandValidator : AbstractValidator<UpdateTitleCommand>
    {
        public UpdateTitleCommandValidator()
        {
            RuleFor(x => x.TitleId)
                .GreaterThan(0).WithMessage("Geçerli bir ünvan ID’si belirtilmelidir.");

            RuleFor(x => x.TitleName)
                .NotEmpty().WithMessage("Ünvan adı boş olamaz.")
                .MaximumLength(100).WithMessage("Ünvan adı en fazla 100 karakter olabilir.");
        }
    }
}
