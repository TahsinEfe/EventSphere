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
    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, List<DepartmentDto>>
    {
        private readonly IDepartmentService _departmentService;

        public GetAllDepartmentsQueryHandler(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task<List<DepartmentDto>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _departmentService.GetAllAsync();

            return entities.Select(d => new DepartmentDto
            {
                DepartmentId = d.DepId,
                DepartmentName = d.DepName
            }).ToList();
        }
    }
}
