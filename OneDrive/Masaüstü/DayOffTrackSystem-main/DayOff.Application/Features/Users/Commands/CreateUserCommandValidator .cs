using DayOff.Application.DTOs.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DayOff.Application.Features.Users.Commands
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.CreateUserDto)
                .NotNull().WithMessage("Kullanıcı bilgileri boş olamaz.")
                .SetValidator(new CreateUserDtoValidator());
        }
    }
}