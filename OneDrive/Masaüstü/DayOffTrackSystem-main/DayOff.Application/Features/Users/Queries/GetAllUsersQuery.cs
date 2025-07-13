using DayOff.Application.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DayOff.Application.Features.Users.Queries
{
    public class GetAllUsersQuery : IRequest<List<UserDto>>
    {
    }
}
