using DayOff.Application.DTOs.Users;
using MediatR;
using System.Security.Cryptography.X509Certificates;

namespace DayOff.Application.Features.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public int UserId { get; set; }

        public GetUserByIdQuery(int userId)
        {
            UserId = userId;
           
        }
    }
}
