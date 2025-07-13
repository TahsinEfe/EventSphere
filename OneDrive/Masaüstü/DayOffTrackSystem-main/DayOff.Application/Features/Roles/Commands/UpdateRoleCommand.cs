using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Roles.Commands
{
    public class UpdateRoleCommand : IRequest<bool>
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public UpdateRoleCommand(int roleId, string roleName)
        {
            RoleId = roleId;
            RoleName = roleName;
        }
    }
}
