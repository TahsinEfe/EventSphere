using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DayOff.Application.DTOs.Users;

namespace DayOff.Application.Features.Users.Commands
{
    public class CreateUserCommand : IRequest<int>
    {
        public CreateUserDto CreateUserDto { get; set; }
        public CreateUserCommand() { }


        public CreateUserCommand(CreateUserDto dto)
        {
            CreateUserDto = dto;
        }
    }
}
