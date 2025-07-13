using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.Departments
{
    public class CreateDepartmentDtoValidator : AbstractValidator<CreateDepartmentDto>
    {
        public CreateDepartmentDtoValidator()
        {
            RuleFor(x => x.DepartmentName)
            .NotEmpty().WithMessage("Departman adı boş olamaz.")
            .MaximumLength(155).WithMessage("Departman adı en fazla 155 karakter olabilir.");
        }
    }
}
