using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using EventSphere.Models;
using EventSphere.ViewModels;

namespace EventSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbacksController : ControllerBase
    {
        private readonly EventSphereDbContext _context;

        public FeedbacksController(EventSphereDbContext context)
        {
            _context = context;
        }

        // GET: api/feedbacks
        [HttpGet]
        public async System.Threading.Tasks.Task<IActionResult> GetAll()
        {
            var feedbacks = await System.Threading.Tasks.Task.Run(() =>
                _context.Feedbacks
                    .FromSqlRaw("EXEC sp_GetAllFeedbacks")
                    .AsEnumerable()
                    .Select(f => new FeedbackDto
                    {
                        FeedbackId = (int)f.FeedbackId,
                        EventId = f.EventId,
                        UserId = (int)(f.UserId ?? 0),
                        Rating = (int)(f.Rating ?? 0),
                        Comments = f.Comments,
                        SubmissionDate = f.SubmissionDate,
                        UserName = f.User?.FirstName + " " + f.User?.LastName,
                        EventName = f.Event?.Name
                    })
                    .ToList()
            );

            return Ok(feedbacks);
        }

        // GET: api/feedbacks/5
        [HttpGet("{id}")]
        public async System.Threading.Tasks.Task<IActionResult> GetById(int id)
        {
            var param = new SqlParameter("@FeedbackId", id);

            var feedbacks = await _context.Feedbacks
                .FromSqlRaw("EXEC sp_GetFeedbackById @FeedbackId", param)
                .ToListAsync();

            var f = feedbacks.FirstOrDefault();
            if (f == null)
                return NotFound();

            var dto = new FeedbackDto
            {
                FeedbackId = (int)f.FeedbackId,
                EventId = f.EventId,
                UserId = (int)f.UserId,
                Rating = (int)f.Rating,
                Comments = f.Comments,
                SubmissionDate = f.SubmissionDate,
                UserName = f.User != null ? f.User.FirstName + " " + f.User.LastName : null,
                EventName = f.Event != null ? f.Event.Name : null
            };

            return Ok(dto);
        }

        // POST: api/feedbacks?userId=1
        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> Create([FromBody] FeedbackDto dto, [FromQuery] int userId)
        {
            var parameters = new[]
            {
                new SqlParameter("@EventID", dto.EventId),
                new SqlParameter("@UserID", userId),
                new SqlParameter("@Rating", dto.Rating),
                new SqlParameter("@Comments", dto.Comments ?? (object)DBNull.Value)
            };

            var feedbackIdResult = await _context.Feedbacks
                .FromSqlRaw("EXEC sp_CreateFeedback @EventID, @UserID, @Rating, @Comments", parameters)
                .ToListAsync();

            var newFeedback = feedbackIdResult.FirstOrDefault();
            if (newFeedback == null)
                return StatusCode(500, "Yorum oluşturulamadı.");

            dto.FeedbackId = (int)newFeedback.FeedbackId;
            dto.SubmissionDate = newFeedback.SubmissionDate;

            return CreatedAtAction(nameof(GetById), new { id = dto.FeedbackId }, dto);
        }

        // PUT: api/feedbacks/5?userId=1
        [HttpPut("{id}")]
        public async System.Threading.Tasks.Task<IActionResult> Update(int id, [FromBody] FeedbackDto dto, [FromQuery] int userId)
        {
            if (id != dto.FeedbackId)
                return BadRequest("ID uyuşmazlığı");

            var feedback = await _context.Feedbacks.FindAsync((long)id);
            if (feedback == null)
                return NotFound();

            if (feedback.UserId != userId)
                return Forbid("Sadece kendi yorumunuzu güncelleyebilirsiniz.");

            var parameters = new[]
            {
                new SqlParameter("@FeedbackId", id),
                new SqlParameter("@UserID", userId),
                new SqlParameter("@Rating", dto.Rating),
                new SqlParameter("@Comments", dto.Comments ?? (object)DBNull.Value)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_UpdateFeedback @FeedbackId, @UserID, @Rating, @Comments", parameters);

            return NoContent();
        }

        // DELETE: api/feedbacks/5?userId=1
        [HttpDelete("{id}")]
        public async System.Threading.Tasks.Task<IActionResult> Delete(int id, [FromQuery] int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            var isAdmin = user != null && user.RoleId == 1;

            var parameters = new[]
            {
                new SqlParameter("@FeedbackId", id),
                new SqlParameter("@UserID", userId),
                new SqlParameter("@IsAdmin", isAdmin)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_DeleteFeedback @FeedbackId, @UserID, @IsAdmin", parameters);

            return NoContent();
        }
    }
}
