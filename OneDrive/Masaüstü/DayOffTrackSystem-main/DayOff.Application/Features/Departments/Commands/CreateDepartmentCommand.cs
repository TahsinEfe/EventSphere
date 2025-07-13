using DayOff.Application.DTOs.Departments;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Departments.Commands
{
    public class CreateDepartmentCommand : IRequest<int>
    {
        public CreateDepartmentDto CreateDto { get; set; }

        public CreateDepartmentCommand(CreateDepartmentDto dto)
        {
            CreateDto = dto;
        }
    }
}
