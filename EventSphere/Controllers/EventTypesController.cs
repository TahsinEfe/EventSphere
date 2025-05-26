using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventSphere.Models;
using EventSphere.ViewModels;

namespace EventSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventTypesController : ControllerBase
    {
        private readonly EventSphereDbContext _context;

        public EventTypesController(EventSphereDbContext context)
        {
            _context = context;
        }

        // GET: api/eventtypes
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var types = await _context.EventTypes
                .Select(t => new EventTypeDto
                {
                    EventTypeId = t.EventTypeId,
                    TypeName = t.TypeName
                })
                .ToListAsync();

            return Ok(types);
        }

        // GET: api/eventtypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var type = await _context.EventTypes
                .Where(t => t.EventTypeId == id)
                .Select(t => new EventTypeDto
                {
                    EventTypeId = t.EventTypeId,
                    TypeName = t.TypeName
                })
                .FirstOrDefaultAsync();

            if (type == null)
                return NotFound();

            return Ok(type);
        }

        // POST: api/eventtypes?userId=1
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] int userId, EventTypeDto dto)
        {
            if (!await IsAdmin(userId))
                return StatusCode(403, "Sadece admin kullanıcılar ekleyebilir.");

            var type = new EventType
            {
                TypeName = dto.TypeName
            };

            _context.EventTypes.Add(type);
            await _context.SaveChangesAsync();

            dto.EventTypeId = type.EventTypeId;
            return CreatedAtAction(nameof(GetById), new { id = type.EventTypeId }, dto);
        }

        // PUT: api/eventtypes/5?userId=1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromQuery] int userId, EventTypeDto dto)
        {
            if (id != dto.EventTypeId)
                return BadRequest();

            if (!await IsAdmin(userId))
                return StatusCode(403, "Sadece admin kullanıcılar güncelleyebilir.");

            var type = await _context.EventTypes.FindAsync(id);
            if (type == null)
                return NotFound();

            type.TypeName = dto.TypeName;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/eventtypes/5?userId=1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] int userId)
        {
            if (!await IsAdmin(userId))
                return StatusCode(403, "Sadece admin kullanıcılar silebilir.");

            var type = await _context.EventTypes.FindAsync(id);
            if (type == null)
                return NotFound();

            _context.EventTypes.Remove(type);
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
