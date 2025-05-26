using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetAll()
        {
            var feedbacks = await _context.Feedbacks
                .Include(f => f.User)
                .Include(f => f.Event)
                .Select(f => new FeedbackDto
                {
                    FeedbackId = (int)f.FeedbackId,
                    EventId = f.EventId,
                    UserId = (int)f.UserId,
                    Rating = (int)f.Rating,
                    Comments = f.Comments,
                    SubmissionDate = f.SubmissionDate,
                    UserName = f.User.FirstName + " " + f.User.LastName,
                    EventName = f.Event.Name
                })
                .ToListAsync();

            return Ok(feedbacks);
        }

        // GET: api/feedbacks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var f = await _context.Feedbacks
                .Include(f => f.User)
                .Include(f => f.Event)
                .Where(f => f.FeedbackId == id)
                .Select(f => new FeedbackDto
                {
                    FeedbackId = (int)f.FeedbackId,
                    EventId = f.EventId,
                    UserId = (int)f.UserId,
                    Rating = (int)f.Rating,
                    Comments = f.Comments,
                    SubmissionDate = f.SubmissionDate,
                    UserName = f.User.FirstName + " " + f.User.LastName,
                    EventName = f.Event.Name
                })
                .FirstOrDefaultAsync();

            if (f == null)
                return NotFound();

            return Ok(f);
        }

        // POST: api/feedbacks?userId=1
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FeedbackDto dto, [FromQuery] int userId)
        {
            var feedback = new Feedback
            {
                EventId = dto.EventId,
                UserId = userId,
                Rating = dto.Rating,
                Comments = dto.Comments,
                SubmissionDate = DateTime.Now
            };

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            dto.FeedbackId = (int)feedback.FeedbackId;
            dto.SubmissionDate = feedback.SubmissionDate;

            return CreatedAtAction(nameof(GetById), new { id = feedback.FeedbackId }, dto);
        }

        // PUT: api/feedbacks/5?userId=1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FeedbackDto dto, [FromQuery] int userId)
        {
            if (id != dto.FeedbackId)
                return BadRequest("ID uyuşmazlığı");

            var feedback = await _context.Feedbacks.FindAsync((long)id);
            if (feedback == null)
                return NotFound();

            if (feedback.UserId != userId)
                return Forbid("Sadece kendi yorumunuzu güncelleyebilirsiniz.");

            feedback.Rating = dto.Rating;
            feedback.Comments = dto.Comments;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/feedbacks/5?userId=1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] int userId)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null)
                return NotFound();

            var user = await _context.Users.FindAsync(userId);
            var isAdmin = user != null && user.RoleId == 1;

            if (!isAdmin && feedback.UserId != userId)
                return Forbid("Sadece kendi yorumunuzu silebilir ya da admin olmalısınız.");

            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
