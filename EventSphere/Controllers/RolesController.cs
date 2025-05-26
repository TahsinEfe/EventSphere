using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventSphere.Models;
using EventSphere.ViewModels;

namespace EventSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly EventSphereDbContext _context;

        public RolesController(EventSphereDbContext context)
        {
            _context = context;
        }

        // GET: api/roles
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _context.Roles
                .Select(r => new RoleDto
                {
                    RoleId = r.RoleId,
                    RoleName = r.RoleName
                })
                .ToListAsync();

            return Ok(roles);
        }

        // GET: api/roles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _context.Roles
                .Where(r => r.RoleId == id)
                .Select(r => new RoleDto
                {
                    RoleId = r.RoleId,
                    RoleName = r.RoleName
                })
                .FirstOrDefaultAsync();

            if (role == null)
                return NotFound();

            return Ok(role);
        }

        // POST: api/roles?userId=1
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] int userId, RoleDto dto)
        {
            if (!await IsAdmin(userId))
                return StatusCode(403, "Sadece admin kullanıcılar ekleyebilir.");

            var role = new Role
            {
                RoleName = dto.RoleName
            };

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            dto.RoleId = role.RoleId;
            return CreatedAtAction(nameof(GetById), new { id = role.RoleId }, dto);
        }

        // PUT: api/roles/5?userId=1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromQuery] int userId, RoleDto dto)
        {
            if (id != dto.RoleId)
                return BadRequest();

            if (!await IsAdmin(userId))
                return StatusCode(403, "Sadece admin kullanıcılar güncelleyebilir.");

            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                return NotFound();

            role.RoleName = dto.RoleName;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/roles/5?userId=1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] int userId)
        {
            if (!await IsAdmin(userId))
                return StatusCode(403, "Sadece admin kullanıcılar silebilir.");

            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                return NotFound();

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> IsAdmin(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user != null && user.RoleId == 1;
        }
    }
}