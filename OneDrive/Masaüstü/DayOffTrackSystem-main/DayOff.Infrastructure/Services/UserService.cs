using DayOff.Application.Interfaces;
using DayOff.Application.Interfaces.Repositories;
using DayOff.Application.Interfaces.Services;
using DayOff.Application.Interfaces.Services;
using DayOff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOff.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<int> CreateAsync(DyUser user)
        {
            return _userRepository.CreateAsync(user); // sadece domain modeli ile çalış
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _userRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<DyUser>> GetAllAsync()
        {
            return _userRepository.GetAllAsync();
        }

        public Task<DyUser?> GetByIdAsync(int id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        public Task<bool> UpdateAsync(DyUser user)
        {
            return _userRepository.UpdateAsync(user);
        }
    }

}
