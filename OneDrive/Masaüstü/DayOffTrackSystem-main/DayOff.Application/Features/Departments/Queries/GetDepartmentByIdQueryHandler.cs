using DayOff.Application.DTOs.Departments;
using DayOff.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Departments.Queries
{
    public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, DepartmentDto?>
    {
        private readonly IDepartmentService _departmentService;

        public GetDepartmentByIdQueryHandler(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task<DepartmentDto?> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _departmentService.GetByIdAsync(request.Id);
            if (entity == null) return null;

            return new DepartmentDto
            {
                DepartmentId = entity.DepId,
                DepartmentName = entity.DepName
            };
        }
    }
}
