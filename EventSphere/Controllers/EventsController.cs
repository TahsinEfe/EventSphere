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
                .ToListAsync();

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
                ImageUrl = e.ImageUrl,
                Description = e.Description,
                Location = e.Location

            }).ToList();

            return Ok(dtoList);
        }

        // GET: api/events/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var ev = await _context.Events
                //.Include(e => e.Address) // <-- ÇIKARILDI!
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
                ImageUrl = ev.ImageUrl,
                Location = ev.Location,
                Description = ev.Description
            };

            return Ok(dto);
        }

        // POST: api/events
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] EventCreateRequest model)
        {
            if (!await HasAccess(model.OrganizationId, model.OrganizerUserId))
                return StatusCode(403, "Yetkiniz yok.");

            // Tarih parse işlemleri
            if (!DateTime.TryParse(model.StartDateTime.ToString(), out DateTime parsedStartDate))
                return BadRequest("Geçersiz başlangıç tarihi.");

            if (!DateTime.TryParse(model.EndDateTime.ToString(), out DateTime parsedEndDate))
                return BadRequest("Geçersiz bitiş tarihi.");

            DateTime? parsedDeadline = null;
            if (model.RegistrationDeadline != null)
            {
                if (!DateTime.TryParse(model.RegistrationDeadline.ToString(), out DateTime tempDeadline))
                    return BadRequest("Geçersiz kayıt bitiş tarihi.");
                parsedDeadline = tempDeadline;
            }

            // Görsel dosyasını kaydetme
            string savedFileName = null;
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                savedFileName = "/uploads/" + uniqueFileName;
            }

            var ev = new Event
            {
                OrganizationId = model.OrganizationId,
                Name = model.Name,
                StartDateTime = parsedStartDate,
                EndDateTime = parsedEndDate,
                EventTypeId = model.EventTypeId,
                EventStatusId = model.EventStatusId,
                OrganizerUserId = model.OrganizerUserId,
                MaxAttendees = model.MaxAttendees,
                IsPublic = model.IsPublic,
                RegistrationDeadline = parsedDeadline,
                Location = model.Location,
                ImageUrl = savedFileName,
                Description = model.Description
            };

            _context.Events.Add(ev);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvent), new { id = ev.EventId }, new { id = ev.EventId });
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
            ev.Location = dto.Location;
            ev.IsPublic = dto.IsPublic;
            ev.RegistrationDeadline = dto.RegistrationDeadline;
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
