using DayOff.Application.DTOs.Users;
using DayOff.Application.Interfaces.Repositories;
using DayOff.Application.Interfaces.Services;
using MediatR;

namespace DayOff.Application.Features.Users.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var entities = await _userRepository.GetAllAsync(); // List<DyUser> 

            var users = entities.Select(u => new UserDto
            {
                UserId = u.UserId,
                FirstName = u.FirstName,
                LastName = u.LastName,
                TcNo = u.TcNo,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Neighborhood = u.Neighborhood,
                Street = u.Street,
                Building = u.Building,
                District = u.District,
                City = u.City,
                GenderId = u.GenderId,
                GenderName = null,
                DateOfBirth = u.DateOfBirth,
                EmploymentDate = u.EmploymentDate,
                IsActive = u.IsActive,
                RoleId = u.RoleId,
                RoleName = null,
                DepartmentId = u.DepartmentId,
                DepartmentName = null,
                TitleId = u.TitleId,
                TitleName = null
            }).ToList();

            return users;
        }
    }
}
