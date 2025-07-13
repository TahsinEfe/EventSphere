using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.DTOs.Departments
{
    public class UpdateDepartmentDtoValidator : AbstractValidator<UpdateDepartmentDto>
    {
        public UpdateDepartmentDtoValidator()
        {
            RuleFor(x => x.DepartmentId)
                .GreaterThan(0).WithMessage("Geçerli bir departman ID'si belirtilmelidir.");

            RuleFor(x => x.DepartmentName)
                .NotEmpty().WithMessage("Departman adı boş olamaz.")
                .MaximumLength(155).WithMessage("Departman adı en fazla 155 karakter olabilir.");
        }
    }
}