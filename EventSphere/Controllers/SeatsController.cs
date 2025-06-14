using EventSphere.Models;
using EventSphere.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeatsController : ControllerBase
    {
        private readonly EventSphereDbContext _context;

        public SeatsController(EventSphereDbContext context)
        {
            _context = context;
        }

        // GET: api/seats
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var seats = await _context.Seats
                .Include(s => s.Event)
                .Select(s => new SeatDto
                {
                    SeatId = s.SeatId,
                    EventId = s.EventId,
                    Section = s.Section ?? "",
                    RowNumber = s.RowNumber ?? "",
                    SeatNumber = s.SeatNumber ?? "",
                    IsReserved = s.IsReserved,
                    EventName = s.Event.Name
                })
                .ToListAsync();

            return Ok(seats);
        }

        [HttpGet("by-event/{eventId}")]
        public async Task<IActionResult> GetSeatsByEvent(int eventId)
        {
            var seats = await _context.Seats
                .Include(s => s.Event)
                .Where(s => s.EventId == eventId)
                .Select(s => new SeatDto
                {
                    SeatId = s.SeatId,
                    EventId = s.EventId,
                    Section = s.Section ?? "",
                    RowNumber = s.RowNumber ?? "",
                    SeatNumber = s.SeatNumber ?? "",
                    IsReserved = s.IsReserved,
                    EventName = s.Event.Name
                })
                .ToListAsync();

            return Ok(seats);
        }

        // GET: api/seats/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var s = await _context.Seats
                .Include(s => s.Event)
                .Where(s => s.SeatId == id)
                .Select(s => new SeatDto
                {
                    SeatId = s.SeatId,
                    EventId = s.EventId,
                    Section = s.Section ?? "",
                    RowNumber = s.RowNumber ?? "",
                    SeatNumber = s.SeatNumber ?? "",
                    IsReserved = s.IsReserved,
                    EventName = s.Event.Name
                })
                .FirstOrDefaultAsync();

            if (s == null)
                return NotFound();

            return Ok(s);
        }


        // POST: api/seats?userId=1
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SeatDto dto, [FromQuery] int userId)
        {
            var ev = await _context.Events.FindAsync(dto.EventId);
            if (ev == null)
                return BadRequest("Etkinlik bulunamadı.");

            if (!await HasAccess(userId, ev.OrganizationId))
                return Forbid("Yetkiniz yok.");

            var seat = new Seat
            {
                EventId = dto.EventId,
                Section = dto.Section,
                RowNumber = dto.RowNumber,
                SeatNumber = dto.SeatNumber,
                IsReserved = dto.IsReserved
            };

            _context.Seats.Add(seat);
            await _context.SaveChangesAsync();

            dto.SeatId = seat.SeatId;
            return CreatedAtAction(nameof(GetById), new { id = seat.SeatId }, dto);
        }

        // PUT: api/seats/5?userId=1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SeatDto dto, [FromQuery] int userId)
        {
            if (id != dto.SeatId)
                return BadRequest();

            var ev = await _context.Events.FindAsync(dto.EventId);
            if (ev == null)
                return BadRequest("Etkinlik bulunamadı.");

            if (!await HasAccess(userId, ev.OrganizationId))
                return Forbid("Yetkiniz yok.");

            var seat = await _context.Seats.FindAsync(id);
            if (seat == null)
                return NotFound();

            seat.Section = dto.Section;
            seat.RowNumber = dto.RowNumber;
            seat.SeatNumber = dto.SeatNumber;
            seat.IsReserved = dto.IsReserved;
            seat.EventId = dto.EventId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/seats/5?userId=1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] int userId)
        {
            var seat = await _context.Seats.FindAsync(id);
            if (seat == null)
                return NotFound();

            var ev = await _context.Events.FindAsync(seat.EventId);
            if (ev == null)
                return BadRequest("Etkinlik bulunamadı.");

            if (!await HasAccess(userId, ev.OrganizationId))
                return Forbid("Yetkiniz yok.");

            _context.Seats.Remove(seat);
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
