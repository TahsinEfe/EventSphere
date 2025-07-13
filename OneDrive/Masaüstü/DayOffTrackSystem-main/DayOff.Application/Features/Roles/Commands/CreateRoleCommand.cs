using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Roles.Commands
{
    public class CreateRoleCommand : IRequest<int>
    {
        public string RoleName { get; set; }

        public CreateRoleCommand(string roleName)
        {
            RoleName = roleName;
        }
    }
}
