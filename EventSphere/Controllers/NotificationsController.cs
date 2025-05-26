using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventSphere.Models;

namespace EventSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly EventSphereDbContext _context;

        public NotificationsController(EventSphereDbContext context)
        {
            _context = context;
        }

        // GET: api/notifications?userId=1
        [HttpGet]
        public async Task<IActionResult> GetUserNotifications([FromQuery] int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return Unauthorized();

            if (user.RoleId == 1)
            {
                // Admin tüm bildirimleri görebilir
                return Ok(await _context.Notifications.ToListAsync());
            }

            // Normal kullanıcı sadece kendi bildirimlerini görür
            return Ok(await _context.Notifications
                .Where(n => n.UserId == userId)
                .ToListAsync());
        }

        // GET: api/notifications/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
                return NotFound();

            return Ok(notification);
        }

        // POST: api/notifications?userId=1
        [HttpPost]
        public async Task<IActionResult> Create(Notification notification, [FromQuery] int userId)
        {
            notification.UserId = userId;
            notification.CreatedDate = DateTime.Now;
            notification.IsRead = false;

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = notification.NotificationId }, notification);
        }

        // PUT: api/notifications/5?userId=1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Notification notification, [FromQuery] int userId)
        {
            if (id != notification.NotificationId)
                return BadRequest();

            var original = await _context.Notifications.AsNoTracking().FirstOrDefaultAsync(n => n.NotificationId == id);
            if (original == null) return NotFound();

            if (original.UserId != userId)
                return Forbid("Sadece kendi bildirimlerinizi güncelleyebilirsiniz.");

            _context.Entry(notification).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/notifications/5?userId=1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] int userId)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null) return NotFound();

            var user = await _context.Users.FindAsync(userId);
            if (notification.UserId != userId && (user == null || user.RoleId != 1))
                return Forbid("Sadece admin ya da bildirimin sahibi silebilir.");

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return NoContent(); 
        }
    }
}
