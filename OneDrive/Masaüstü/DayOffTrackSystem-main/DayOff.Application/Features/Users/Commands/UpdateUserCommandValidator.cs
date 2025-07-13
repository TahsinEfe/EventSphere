using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Users.Commands
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("Geçerli bir kullanıcı ID'si olmalıdır.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad boş olamaz.")
                .MaximumLength(55);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad boş olamaz.")
                .MaximumLength(55);

            RuleFor(x => x.TcNo)
                .NotEmpty()
                .Length(11).WithMessage("TC Kimlik No 11 haneli olmalıdır.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

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
