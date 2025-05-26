using EventSphere.Models;
using EventSphere.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserDto = EventSphere.ViewModels.UserDto;

namespace EventSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly EventSphereDbContext _context;

        public UsersController(EventSphereDbContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _context.Users
                .Include(u => u.Role)
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    IsActive = u.IsActive,
                    RoleId = u.RoleId,
                    RoleName = u.Role.RoleName
                })
                .ToListAsync();

            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .Where(u => u.UserId == id)
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    IsActive = u.IsActive,
                    RoleId = u.RoleId,
                    RoleName = u.Role.RoleName
                })
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<User>> Register([FromBody] RegisterRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.PasswordHash) ||
                string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest("Zorunlu alanlar boş geçilemez.");
            }

            var exists = await _context.Users
                .AnyAsync(u => u.Username == request.Username || u.Email == request.Email);

            if (exists)
                return BadRequest("Bu kullanıcı adı veya e-posta zaten kayıtlı.");

            var user = new User
            {
                Username = request.Username,
                PasswordHash = request.PasswordHash,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                RoleId = 4,       // Otomatik olarak Attendee
                IsActive = true   
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }



        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDto dto)
        {
            if (id != dto.UserId)
                return BadRequest();

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            user.Username = dto.Username;
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.IsActive = dto.IsActive;
            user.RoleId = dto.RoleId;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
