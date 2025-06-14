using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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

            var parameters = new[]
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@IsAdmin", user.RoleId == 1)
            };

            var notifications = await _context.Notifications
                .FromSqlRaw("EXEC sp_GetNotificationsByUser @UserID, @IsAdmin", parameters)
                .ToListAsync();

            return Ok(notifications);
        }

        // GET: api/notifications/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var param = new SqlParameter("@NotificationId", id);

            var results = await _context.Notifications
                .FromSqlRaw("EXEC sp_GetNotificationById @NotificationId", param)
                .ToListAsync();

            var notification = results.FirstOrDefault();
            if (notification == null)
                return NotFound();

            return Ok(notification);
        }

        // POST: api/notifications?userId=1
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNotificationDto dto, [FromQuery] int userId)
        {
            var parameters = new[]
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@Title", dto.Title),
                new SqlParameter("@Message", dto.Message)
            };

            var result = await _context.Notifications
            .FromSqlRaw("EXEC sp_CreateNotification @UserID, @Title, @Message", parameters)
            .ToListAsync();

            var created = result.FirstOrDefault();
            if (created == null)
                return StatusCode(500, "Bildirim oluşturulamadı.");

            return CreatedAtAction(nameof(GetById), new { id = created.NotificationId }, created);
        }

        // PUT: api/notifications/5?userId=1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateNotificationDto dto, [FromQuery] int userId)
        {
            if (id != dto.NotificationId)
                return BadRequest("ID uyuşmazlığı.");

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return Unauthorized("Kullanıcı bulunamadı.");

            var parameters = new[]
            {
                new SqlParameter("@NotificationId", id),
                new SqlParameter("@UserID", userId),
                new SqlParameter("@IsRead", dto.IsRead),
                new SqlParameter("@IsAdmin", user.RoleId == 1)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_UpdateNotification @NotificationId, @UserID, @IsRead, @IsAdmin", parameters);

            return NoContent();
        }

        // DELETE: api/notifications/5?userId=1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id, [FromQuery] int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return Unauthorized("Kullanıcı bulunamadı.");

            var parameters = new[]
            {
                new SqlParameter("@NotificationId", id),
                new SqlParameter("@UserID", userId),
                new SqlParameter("@IsAdmin", user.RoleId == 1)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_DeleteNotification @NotificationId, @UserID, @IsAdmin", parameters);

            return NoContent();
        }
    }
}
