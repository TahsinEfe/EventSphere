using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventSphere.Models;
using EventSphere.ViewModels;

namespace EventSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskStatusesController : ControllerBase
    {
        private readonly EventSphereDbContext _context;

        public TaskStatusesController(EventSphereDbContext context)
        {
            _context = context;
        }

        // GET: api/taskstatuses
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var statuses = await _context.TaskStatuses
                .Select(s => new TaskStatusDto
                {
                    TaskStatusId = s.TaskStatusId,
                    StatusName = s.StatusName
                })
                .ToListAsync();

            return Ok(statuses);
        }

        // GET: api/taskstatuses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var status = await _context.TaskStatuses
                .Where(s => s.TaskStatusId == id)
                .Select(s => new TaskStatusDto
                {
                    TaskStatusId = s.TaskStatusId,
                    StatusName = s.StatusName
                })
                .FirstOrDefaultAsync();

            if (status == null)
                return NotFound();

            return Ok(status);
        }

        // POST: api/taskstatuses?userId=1
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] int userId, TaskStatusDto dto)
        {
            if (!await IsAdmin(userId))
                return StatusCode(403, "Sadece admin kullanıcılar ekleyebilir.");

            var status = new Models.TaskStatus
            {
                StatusName = dto.StatusName
            };

            _context.TaskStatuses.Add(status);
            await _context.SaveChangesAsync();

            dto.TaskStatusId = status.TaskStatusId;
            return CreatedAtAction(nameof(GetById), new { id = status.TaskStatusId }, dto);
        }

        // PUT: api/taskstatuses/5?userId=1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromQuery] int userId, TaskStatusDto dto)
        {
            if (id != dto.TaskStatusId)
                return BadRequest();

            if (!await IsAdmin(userId))
                return StatusCode(403, "Sadece admin kullanıcılar güncelleyebilir.");

            var status = await _context.TaskStatuses.FindAsync(id);
            if (status == null)
                return NotFound();

            status.StatusName = dto.StatusName;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/taskstatuses/5?userId=1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] int userId)
        {
            if (!await IsAdmin(userId))
                return StatusCode(403, "Sadece admin kullanıcılar silebilir.");

            var status = await _context.TaskStatuses.FindAsync(id);
            if (status == null)
                return NotFound();

            _context.TaskStatuses.Remove(status);
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
