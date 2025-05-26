using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventSphere.Models;
using EventSphere.ViewModels;

namespace EventSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly EventSphereDbContext _context;

        public EventsController(EventSphereDbContext context)
        {
            _context = context;
        }

        // GET: api/events
        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _context.Events
                .Include(e => e.EventType)
                .Include(e => e.EventStatus)
                .Include(e => e.Organization)
                .Include(e => e.Address)
                .ToListAsync(); // 👉 önce veritabanından çek

            var dtoList = events.Select(e => new EventDto
            {
                EventId = e.EventId,
                OrganizationId = e.OrganizationId,
                Name = e.Name,
                StartDateTime = e.StartDateTime,
                EndDateTime = e.EndDateTime,
                EventTypeId = e.EventTypeId,
                EventStatusId = e.EventStatusId,
                OrganizerUserId = e.OrganizerUserId,
                MaxAttendees = e.MaxAttendees,
                IsPublic = e.IsPublic,
                RegistrationDeadline = e.RegistrationDeadline,
                AddressId = e.AddressId,
                Location = e.Address?.Street ?? "Bilinmiyor",
                ImageUrl = e.ImageUrl,
                Description = e.Description,
                City = e.Address?.City ?? "Bilinmiyor",
                Country = e.Address?.Country ?? "Bilinmiyor"
            }).ToList();

            return Ok(dtoList);
        }


        // GET: api/events/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var ev = await _context.Events
                .Include(e => e.Address)
                .FirstOrDefaultAsync(e => e.EventId == id);

            if (ev == null)
                return NotFound();

            var dto = new EventDto
            {
                EventId = ev.EventId,
                OrganizationId = ev.OrganizationId,
                Name = ev.Name,
                StartDateTime = ev.StartDateTime,
                EndDateTime = ev.EndDateTime,
                EventTypeId = ev.EventTypeId,
                EventStatusId = ev.EventStatusId,
                OrganizerUserId = ev.OrganizerUserId,
                MaxAttendees = ev.MaxAttendees,
                IsPublic = ev.IsPublic,
                RegistrationDeadline = ev.RegistrationDeadline,
                AddressId = ev.AddressId,
                Location = ev.Address?.Street ?? "Bilinmiyor",
                ImageUrl = ev.ImageUrl,
                Description = ev.Description,
                City = ev.Address?.City ?? "Bilinmiyor",
                Country = ev.Address?.Country ?? "Bilinmiyor"
            };

            return Ok(dto);
        }


        // POST: api/events
        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventDto dto)
        {
            if (!await HasAccess(dto.OrganizationId, dto.OrganizerUserId))
                return StatusCode(403, "Yetkiniz yok.");

            var ev = new Event
            {
                OrganizationId = dto.OrganizationId,
                Name = dto.Name,
                StartDateTime = dto.StartDateTime,
                EndDateTime = dto.EndDateTime,
                EventTypeId = dto.EventTypeId,
                EventStatusId = dto.EventStatusId,
                OrganizerUserId = dto.OrganizerUserId,
                MaxAttendees = dto.MaxAttendees,
                IsPublic = dto.IsPublic,
                RegistrationDeadline = dto.RegistrationDeadline,
                AddressId = dto.AddressId,
                ImageUrl = dto.ImageUrl,
                Description = dto.Description
            };

            _context.Events.Add(ev);
            await _context.SaveChangesAsync();

            dto.EventId = ev.EventId;
            return CreatedAtAction(nameof(GetEvent), new { id = ev.EventId }, dto);
        }

        // PUT: api/events/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, EventDto dto)
        {
            if (id != dto.EventId)
                return BadRequest();

            if (!await HasAccess(dto.OrganizationId, dto.OrganizerUserId))
                return StatusCode(403, "Yetkiniz yok.");

            var ev = await _context.Events.FindAsync(id);
            if (ev == null)
                return NotFound();

            ev.Name = dto.Name;
            ev.StartDateTime = dto.StartDateTime;
            ev.EndDateTime = dto.EndDateTime;
            ev.EventTypeId = dto.EventTypeId;
            ev.EventStatusId = dto.EventStatusId;
            ev.OrganizerUserId = dto.OrganizerUserId;
            ev.MaxAttendees = dto.MaxAttendees;
            ev.IsPublic = dto.IsPublic;
            ev.RegistrationDeadline = dto.RegistrationDeadline;
            ev.AddressId = dto.AddressId;
            ev.ImageUrl = dto.ImageUrl;
            ev.Description = dto.Description;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null)
                return NotFound();

            if (!await HasAccess(ev.OrganizationId, ev.OrganizerUserId))
                return StatusCode(403, "Yetkiniz yok.");

            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // KULLANICI YETKİSİ KONTROLÜ
        private async Task<bool> HasAccess(int organizationId, int? organizerUserId)
        {
            if (organizerUserId == null)
                return false;

            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserId == organizerUserId);
            if (user == null)
                return false;

            if (user.RoleId == 1) // Admin
                return true;

            return await _context.OrganizationMembers
                .AnyAsync(m => m.OrganizationId == organizationId && m.UserId == organizerUserId);
        }
    }
}
