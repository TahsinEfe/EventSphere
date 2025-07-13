using DayOff.Application.Interfaces.Services;
using DayOff.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Departments.Commands
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, bool>
    {
        private readonly IDepartmentService _departmentService;

        public UpdateDepartmentCommandHandler(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task<bool> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var entity = new DyDepartment
            {
                DepId = request.UpdateDto.DepartmentId,
                DepName = request.UpdateDto.DepartmentName
            };

            return await _departmentService.UpdateAsync(entity);
        }
    }
}
