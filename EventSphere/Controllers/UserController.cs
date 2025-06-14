using EventSphere.Models;
using EventSphere.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            var users = await _context.UserDtos
                .FromSqlRaw("EXEC sp_GetAllUsers")
                .ToListAsync();

            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var param = new SqlParameter("@UserId", id);

            var user = _context.UserDtos
                .FromSqlRaw("EXEC sp_GetUserById @UserId", param)
                .AsNoTracking()
                .AsEnumerable()
                .FirstOrDefault();

            if (user == null)
                return NotFound();

            return Ok(user);
        }


        // POST: api/users
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] RegisterRequest request)
        {
            var parameters = new[]
            {
                new SqlParameter("@Username", request.Username),
                new SqlParameter("@PasswordHash", request.PasswordHash),
                new SqlParameter("@FirstName", request.FirstName ?? (object)DBNull.Value),
                new SqlParameter("@LastName", request.LastName ?? (object)DBNull.Value),
                new SqlParameter("@Email", request.Email),
                new SqlParameter("@RoleId", 4),
                new SqlParameter("@IsActive", true)
            };

            try
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC sp_RegisterUser @Username, @PasswordHash, @FirstName, @LastName, @Email, @RoleId, @IsActive", parameters);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("User registered successfully.");
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDto dto)
        {
            if (id != dto.UserId)
                return BadRequest();

            var parameters = new[]
            {
                new SqlParameter("@UserId", dto.UserId),
                new SqlParameter("@Username", dto.Username),
                new SqlParameter("@FirstName", dto.FirstName ?? (object)DBNull.Value),
                new SqlParameter("@LastName", dto.LastName ?? (object)DBNull.Value),
                new SqlParameter("@Email", dto.Email),
                new SqlParameter("@IsActive", dto.IsActive),
                new SqlParameter("@RoleId", dto.RoleId)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_UpdateUser @UserId, @Username, @FirstName, @LastName, @Email, @IsActive, @RoleId", parameters);

            return NoContent();
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var param = new SqlParameter("@UserId", id);

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_DeleteUser @UserId", param);

            return NoContent();
        }
    }
}