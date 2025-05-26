using EventSphere.Models;
using EventSphere.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationsController : ControllerBase
    {
        private readonly EventSphereDbContext _context;

        public OrganizationsController(EventSphereDbContext context)
        {
            _context = context;
        }

        // GET: api/organizations
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.Organizations
                .Select(o => new OrganizationDto
                {
                    OrganizationId = o.OrganizationId,
                    Name = o.Name,
                    ContactEmail = o.ContactEmail,
                    IsActive = o.IsActive
                })
                .ToListAsync();

            return Ok(list);
        }

        // GET: api/organizations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var org = await _context.Organizations
                .Where(o => o.OrganizationId == id)
                .Select(o => new OrganizationDto
                {
                    OrganizationId = o.OrganizationId,
                    Name = o.Name,
                    ContactEmail = o.ContactEmail,
                    IsActive = o.IsActive
                })
                .FirstOrDefaultAsync();

            if (org == null)
                return NotFound();

            return Ok(org);
        }

        // POST: api/organizations?userId=1
        [HttpPost]
        public async Task<IActionResult> Create(OrganizationDto dto, [FromQuery] int userId)
        {
            if (!await IsAdmin(userId))
                return Forbid("Sadece admin kullanıcılar organizasyon ekleyebilir.");

            var organization = new Organization
            {
                Name = dto.Name,
                ContactEmail = dto.ContactEmail,
                IsActive = dto.IsActive
            };

            _context.Organizations.Add(organization);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = organization.OrganizationId }, new OrganizationDto
            {
                OrganizationId = organization.OrganizationId,
                Name = organization.Name,
                ContactEmail = organization.ContactEmail,
                IsActive = organization.IsActive
            });
        }

        // PUT: api/organizations/5?userId=1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrganizationDto dto, [FromQuery] int userId)
        {
            if (!await IsAdmin(userId))
                return Forbid("Sadece admin kullanıcılar güncelleme yapabilir.");

            var organization = await _context.Organizations.FindAsync(id);
            if (organization == null)
                return NotFound();

            organization.Name = dto.Name;
            organization.ContactEmail = dto.ContactEmail;
            organization.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/organizations/5?userId=1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] int userId)
        {
            if (!await IsAdmin(userId))
                return Forbid("Sadece admin kullanıcılar silebilir.");

            var org = await _context.Organizations.FindAsync(id);
            if (org == null)
                return NotFound();

            _context.Organizations.Remove(org);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> IsAdmin(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user != null && user.RoleId == 1;
        }
    }
}
