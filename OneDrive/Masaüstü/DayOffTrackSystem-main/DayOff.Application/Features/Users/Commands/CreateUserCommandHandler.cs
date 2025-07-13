using DayOff.Application.Interfaces.Services;
using DayOff.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Application.Features.Users.Commands
{
    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserService _userService;


        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CreateUserDto;

            var user = new DyUser
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                TcNo = dto.TcNo,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Neighborhood = dto.Neighborhood,
                Street = dto.Street,
                Building = dto.Building,
                District = dto.District,
                City = dto.City,
                GenderId = dto.GenderId,
                DateOfBirth = dto.DateOfBirth,
                EmploymentDate = dto.EmploymentDate,
                RoleId = dto.RoleId,
                DepartmentId = dto.DepartmentId,
                TitleId = dto.TitleId,
                IsActive = true
            };

            return await _userService.CreateAsync(user);
        }
    }
}