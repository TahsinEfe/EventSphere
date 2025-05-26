using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventSphere.Models;
using EventSphere.ViewModels;

namespace EventSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventStatusesController : ControllerBase
    {
        private readonly EventSphereDbContext _context;

        public EventStatusesController(EventSphereDbContext context)
        {
            _context = context;
        }

        // GET: api/eventstatuses
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var statuses = await _context.EventStatuses
                .Select(s => new EventStatusDto
                {
                    EventStatusId = s.EventStatusId,
                    StatusName = s.StatusName
                })
                .ToListAsync();

            return Ok(statuses);
        }

        // GET: api/eventstatuses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var status = await _context.EventStatuses
                .Where(s => s.EventStatusId == id)
                .Select(s => new EventStatusDto
                {
                    EventStatusId = s.EventStatusId,
                    StatusName = s.StatusName
                })
                .FirstOrDefaultAsync();

            if (status == null)
                return NotFound();

            return Ok(status);
        }

        // POST: api/eventstatuses?userId=1
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] int userId, EventStatusDto dto)
        {
            if (!await IsAdmin(userId))
                return StatusCode(403, "Sadece admin kullanıcılar ekleyebilir.");

            var status = new EventStatus
            {
                StatusName = dto.StatusName
            };

            _context.EventStatuses.Add(status);
            await _context.SaveChangesAsync();

            dto.EventStatusId = status.EventStatusId;
            return CreatedAtAction(nameof(GetById), new { id = status.EventStatusId }, dto);
        }

        // PUT: api/eventstatuses/5?userId=1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromQuery] int userId, EventStatusDto dto)
        {
            if (id != dto.EventStatusId)
                return BadRequest();

            if (!await IsAdmin(userId))
                return StatusCode(403, "Sadece admin kullanıcılar güncelleyebilir.");

            var status = await _context.EventStatuses.FindAsync(id);
            if (status == null)
                return NotFound();

            status.StatusName = dto.StatusName;
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
