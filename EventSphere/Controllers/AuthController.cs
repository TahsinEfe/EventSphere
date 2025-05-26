using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventSphere.Models;
using EventSphere.ViewModels;

namespace EventSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly EventSphereDbContext _context;

        public AuthController(EventSphereDbContext context)
        {
            _context = context;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _context.Users
                .Include(u => u.Role) // 👈 Rol bilgisini çekiyoruz
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
                return Unauthorized("Email bulunamadı.");

            if (user.PasswordHash != request.Password)
                return Unauthorized("Şifre hatalı.");

            // Admin kontrolü
            bool isAdmin = user.Role.RoleName.ToLower() == "admin";

            return Ok(new
            {
                user.UserId,
                user.Username,
                user.Email,
                user.FirstName,
                user.LastName,
                user.RoleId,
                Role = user.Role.RoleName,
                IsAdmin = isAdmin
            });
        }

    }
}