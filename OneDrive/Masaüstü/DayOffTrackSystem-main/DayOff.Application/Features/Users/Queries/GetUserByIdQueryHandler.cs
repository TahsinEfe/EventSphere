using DayOff.Application.DTOs.Users;
using DayOff.Application.Interfaces.Services;
using MediatR;

namespace DayOff.Application.Features.Users.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserService _userService;

        public GetUserByIdQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(request.UserId);

            if (user == null)
                throw new Exception($"User with ID {request.UserId} not found.");

            return new UserDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                TcNo = user.TcNo,
                PhoneNumber = user.PhoneNumber,
                Neighborhood = user.Neighborhood,
                Street = user.Street,
                Building = user.Building,
                District = user.District,
                City = user.City,
                GenderId = user.GenderId,
                DateOfBirth = user.DateOfBirth,
                EmploymentDate = user.EmploymentDate,
                IsActive = user.IsActive,
                RoleId = user.RoleId,
                DepartmentId = user.DepartmentId,
                TitleId = user.TitleId
            };
        }
    }
}
