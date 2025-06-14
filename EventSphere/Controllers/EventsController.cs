using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _context.Events
                .FromSqlRaw("EXEC sp_GetAllEvents")
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var param = new SqlParameter("@EventId", id);

            var events = await _context.Events
                .FromSqlRaw("EXEC sp_GetEventById @EventId", param)
                .ToListAsync();

            var ev = events.FirstOrDefault();

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

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] EventCreateRequest model)
        {
            if (!await HasAccess(model.OrganizationId, model.OrganizerUserId))
                return StatusCode(403, "Yetkiniz yok.");

            if (!DateTime.TryParse(model.StartDateTime.ToString(), out DateTime parsedStartDate))
                return BadRequest("Geçersiz başlangıç tarihi.");

            if (!DateTime.TryParse(model.EndDateTime.ToString(), out DateTime parsedEndDate))
                return BadRequest("Geçersiz bitiş tarihi.");

            DateTime? parsedDeadline = null;
            if (model.RegistrationDeadline != null &&
                DateTime.TryParse(model.RegistrationDeadline.ToString(), out DateTime tempDeadline))
                parsedDeadline = tempDeadline;

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

            var parameters = new[]
            {
                new SqlParameter("@OrganizationId", model.OrganizationId),
                new SqlParameter("@Name", model.Name),
                new SqlParameter("@StartDateTime", parsedStartDate),
                new SqlParameter("@EndDateTime", parsedEndDate),
                new SqlParameter("@EventTypeId", model.EventTypeId),
                new SqlParameter("@EventStatusId", model.EventStatusId),
                new SqlParameter("@OrganizerUserId", model.OrganizerUserId.HasValue ? (object)model.OrganizerUserId.Value : DBNull.Value),
                new SqlParameter("@MaxAttendees", model.MaxAttendees.HasValue ? (object)model.MaxAttendees.Value : DBNull.Value),
                new SqlParameter("@IsPublic", model.IsPublic),
                new SqlParameter("@RegistrationDeadline", parsedDeadline ?? (object)DBNull.Value),
                new SqlParameter("@Location", model.Location ?? (object)DBNull.Value),
                new SqlParameter("@ImageUrl", savedFileName ?? (object)DBNull.Value),
                new SqlParameter("@Description", model.Description ?? (object)DBNull.Value)
            };


            await _context.Database.ExecuteSqlRawAsync("EXEC sp_CreateEvent @OrganizationId, @Name, @StartDateTime, @EndDateTime, @EventTypeId, @EventStatusId, @OrganizerUserId, @MaxAttendees, @IsPublic, @RegistrationDeadline, @Location, @ImageUrl, @Description", parameters);

            return Ok("Etkinlik başarıyla oluşturuldu.");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, EventDto dto)
        {
            if (id != dto.EventId)
                return BadRequest();

            if (!await HasAccess(dto.OrganizationId, dto.OrganizerUserId))
                return StatusCode(403, "Yetkiniz yok.");

            var parameters = new[]
            {
                new SqlParameter("@EventId", dto.EventId),
                new SqlParameter("@OrganizationId", dto.OrganizationId),
                new SqlParameter("@Name", dto.Name),
                new SqlParameter("@StartDateTime", dto.StartDateTime),
                new SqlParameter("@EndDateTime", dto.EndDateTime),
                new SqlParameter("@EventTypeId", dto.EventTypeId),
                new SqlParameter("@EventStatusId", dto.EventStatusId),
                new SqlParameter("@OrganizerUserId", dto.OrganizerUserId.HasValue ? (object)dto.OrganizerUserId.Value : DBNull.Value),
                new SqlParameter("@MaxAttendees", dto.MaxAttendees.HasValue ? (object)dto.MaxAttendees.Value : DBNull.Value),
                new SqlParameter("@IsPublic", dto.IsPublic),
                new SqlParameter("@RegistrationDeadline", dto.RegistrationDeadline ?? (object)DBNull.Value),
                new SqlParameter("@Location", dto.Location ?? (object)DBNull.Value),
                new SqlParameter("@ImageUrl", dto.ImageUrl ?? (object)DBNull.Value),
                new SqlParameter("@Description", dto.Description ?? (object)DBNull.Value)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_UpdateEvent @EventId, @OrganizationId, @Name, @StartDateTime, @EndDateTime, @EventTypeId, @EventStatusId, @OrganizerUserId, @MaxAttendees, @IsPublic, @RegistrationDeadline, @Location, @ImageUrl, @Description", parameters);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var param = new SqlParameter("@EventId", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_DeleteEvent @EventId", param);
            return NoContent();
        }

        private async Task<bool> HasAccess(int organizationId, int? organizerUserId)
        {
            if (organizerUserId == null)
                return false;

            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserId == organizerUserId);
            if (user == null)
                return false;

            if (user.RoleId == 1)
                return true;

            return await _context.OrganizationMembers.AnyAsync(m => m.OrganizationId == organizationId && m.UserId == organizerUserId);
        }
    }
}
