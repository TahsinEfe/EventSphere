using DayOff.Application.Features.Titles.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.Titles
{
    public class CreateTitleCommandValidator : AbstractValidator<CreateTitleCommand>
    {
        public CreateTitleCommandValidator()
        {
            RuleFor(x => x.TitleName)
                .NotEmpty().WithMessage("Ünvan adı boş olamaz.")
                .MaximumLength(100).WithMessage("Ünvan adı en fazla 100 karakter olabilir.");
        }
    }
}
