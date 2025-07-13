using DayOff.Application.DTOs.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Users.Commands
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad boş olamaz.")
                .MaximumLength(55);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad boş olamaz.")
                .MaximumLength(55);

            RuleFor(x => x.TcNo)
                .NotEmpty().WithMessage("TC Kimlik No zorunludur.")
                .Length(11).WithMessage("TC Kimlik No 11 haneli olmalıdır.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta zorunludur.")
                .EmailAddress().WithMessage("Geçerli bir e-posta giriniz.");

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(25);

            RuleFor(x => x.EmploymentDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Today);

            RuleFor(x => x.RoleId)
                .GreaterThan(0);
        }
    }
}
