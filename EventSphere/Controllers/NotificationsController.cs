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
        public async Task<IActionResult> Create([FromBody] CreateNotificationDto dto, [FromQuery] int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound("Kullanıcı bulunamadı.");

            var notification = new Notification
            {
                Title = dto.Title,
                Message = dto.Message,
                UserId = userId,
                CreatedDate = DateTime.Now,
                IsRead = false
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = notification.NotificationId }, notification);
        }


        // PUT: api/notifications/5?userId=1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateNotificationDto dto, [FromQuery] int userId)
        {
            if (id != dto.NotificationId)
                return BadRequest();

            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
                return NotFound();

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return Unauthorized("Kullanıcı bulunamadı.");

            if (notification.UserId != userId && user.RoleId != 1)
                return Unauthorized("Sadece kendi bildiriminizi veya admin yetkiniz varsa düzenleyebilirsiniz.");

            notification.IsRead = dto.IsRead;
            await _context.SaveChangesAsync();

            return NoContent();
        }



        // DELETE: api/notifications/5?userId=1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id, [FromQuery] int userId)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
                return NotFound();

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return Unauthorized("Kullanıcı bulunamadı.");

            // Rolü kontrol et (admin = 1)
            if (notification.UserId != userId && user.RoleId != 1)
                return StatusCode(403, "Sadece kendi bildiriminizi veya admin olarak silebilirsiniz.");

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
