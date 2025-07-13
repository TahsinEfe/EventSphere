using DayOff.Application.DTOs.Departments;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Departments.Queries
{
    public class GetAllDepartmentsQuery : IRequest<List<DepartmentDto>> { }

}
