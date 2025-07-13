using DayOff.Application.DTOs.Departments;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Departments.Commands
{
    public class UpdateDepartmentCommand : IRequest<bool>
    {
        public UpdateDepartmentDto UpdateDto { get; set; }

        public UpdateDepartmentCommand(UpdateDepartmentDto dto)
        {
            UpdateDto = dto;
        }
    }
}
