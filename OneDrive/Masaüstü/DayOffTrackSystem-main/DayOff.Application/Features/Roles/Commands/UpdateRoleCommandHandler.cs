using DayOff.Application.Interfaces.Services;
using DayOff.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Roles.Commands
{
    public class UpdateRoleCommandHandler :IRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly IRoleService _roleService;

        public UpdateRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new DyRole { RoleId = request.RoleId, RoleName = request.RoleName };
            return await _roleService.UpdateAsync(role);
        }
    }
}
