using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Departments.Commands
{
    public class DeleteDepartmentCommand : IRequest<bool>
    {
        public int DepartmentId { get; set; }

        public DeleteDepartmentCommand(int departmentId)
        {
            DepartmentId = departmentId;
        }
    }
}
