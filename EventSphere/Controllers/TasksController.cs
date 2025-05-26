using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventSphere.Models;
using EventSphere.ViewModels;

namespace EventSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly EventSphereDbContext _context;

        public TasksController(EventSphereDbContext context)
        {
            _context = context;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _context.Tasks
                .Include(t => t.Event)
                .Include(t => t.AssignedUser)
                .Include(t => t.TaskStatus)
                .Select(t => new TaskDto
                {
                    TaskId = t.TaskId,
                    EventId = t.EventId,
                    Title = t.Title,
                    AssignedUserId = t.AssignedUserId,
                    DueDate = t.DueDate,
                    TaskStatusId = t.TaskStatusId
                })
                .ToListAsync();

            return Ok(tasks);
        }

        // GET: api/tasks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _context.Tasks
                .Where(t => t.TaskId == id)
                .Select(t => new TaskDto
                {
                    TaskId = t.TaskId,
                    EventId = t.EventId,
                    Title = t.Title,
                    AssignedUserId = t.AssignedUserId,
                    DueDate = t.DueDate,
                    TaskStatusId = t.TaskStatusId
                })
                .FirstOrDefaultAsync();

            if (task == null)
                return NotFound();

            return Ok(task);
        }

        // POST: api/tasks?userId=1
        [HttpPost]
        public async Task<IActionResult> Create(TaskDto dto, [FromQuery] int userId)
        {
            var ev = await _context.Events.FindAsync(dto.EventId);
            if (ev == null)
                return BadRequest("Etkinlik bulunamadı.");

            if (!await HasAccess(userId, ev.OrganizationId))
                return Forbid("Yetkiniz yok.");

            var task = new Models.Task
            {
                EventId = (int)dto.EventId,
                Title = dto.Title,
                AssignedUserId = dto.AssignedUserId,
                DueDate = (DateTime)dto.DueDate,
                TaskStatusId = (int)dto.TaskStatusId
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            dto.TaskId = task.TaskId;
            return CreatedAtAction(nameof(GetById), new { id = task.TaskId }, dto);
        }

        // PUT: api/tasks/5?userId=1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskDto dto, [FromQuery] int userId)
        {
            if (id != dto.TaskId)
                return BadRequest();

            var ev = await _context.Events.FindAsync(dto.EventId);
            if (ev == null)
                return BadRequest("Etkinlik bulunamadı.");

            if (!await HasAccess(userId, ev.OrganizationId))
                return Forbid("Yetkiniz yok.");

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            task.Title = dto.Title;
            task.AssignedUserId = dto.AssignedUserId;
            task.DueDate = dto.DueDate;
            task.TaskStatusId = (int)dto.TaskStatusId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/tasks/5?userId=1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] int userId)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            var ev = await _context.Events.FindAsync(task.EventId);
            if (ev == null)
                return BadRequest("Etkinlik bulunamadı.");

            if (!await HasAccess(userId, ev.OrganizationId))
                return Forbid("Yetkiniz yok.");

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> HasAccess(int userId, int organizationId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            if (user.RoleId == 1)
                return true;

            return await _context.OrganizationMembers
                .AnyAsync(m => m.UserId == userId && m.OrganizationId == organizationId);
        }
    }
}
