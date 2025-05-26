using EventSphere.Models;
using EventSphere.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationMembersController : ControllerBase
    {
        private readonly EventSphereDbContext _context;

        public OrganizationMembersController(EventSphereDbContext context)
        {
            _context = context;
        }

        // GET: api/organizationmembers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.OrganizationMembers
                .Include(om => om.Organization)
                .Include(om => om.User)
                .Select(om => new OrganizationMemberDto
                {
                    MemberId = om.MemberId,
                    OrganizationId = om.OrganizationId,
                    UserId = om.UserId,
                    JoinDate = om.JoinDate,
                    IsAdmin = om.IsAdmin,
                    OrganizationName = om.Organization.Name,
                    UserName = om.User.FirstName + " " + om.User.LastName
                })
                .ToListAsync();

            return Ok(list);
        }

        // GET: api/organizationmembers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var member = await _context.OrganizationMembers
                .Include(om => om.Organization)
                .Include(om => om.User)
                .Where(om => om.MemberId == id)
                .Select(om => new OrganizationMemberDto
                {
                    MemberId = om.MemberId,
                    OrganizationId = om.OrganizationId,
                    UserId = om.UserId,
                    JoinDate = om.JoinDate,
                    IsAdmin = om.IsAdmin,
                    OrganizationName = om.Organization.Name,
                    UserName = om.User.FirstName + " " + om.User.LastName
                })
                .FirstOrDefaultAsync();

            if (member == null)
                return NotFound();

            return Ok(member);
        }

        // POST: api/organizationmembers?userId=1
        [HttpPost]
        public async Task<IActionResult> Create(OrganizationMemberDto dto, [FromQuery] int userId)
        {
            if (!await IsAdmin(userId))
                return Forbid("Sadece admin kullanıcılar organizasyon üyesi ekleyebilir.");

            // Check if organization exists
            var orgExists = await _context.Organizations.AnyAsync(o => o.OrganizationId == dto.OrganizationId);
            if (!orgExists)
                return BadRequest("Belirtilen organizasyon bulunamadı.");

            // Check if user exists
            var userExists = await _context.Users.AnyAsync(u => u.UserId == dto.UserId);
            if (!userExists)
                return BadRequest("Belirtilen kullanıcı bulunamadı.");

            // Check if membership already exists
            var existingMember = await _context.OrganizationMembers
                .AnyAsync(om => om.OrganizationId == dto.OrganizationId && om.UserId == dto.UserId);
            if (existingMember)
                return BadRequest("Bu kullanıcı zaten bu organizasyonun üyesi.");

            var organizationMember = new OrganizationMember
            {
                OrganizationId = dto.OrganizationId,
                UserId = dto.UserId,
                JoinDate = DateTime.Now,
                IsAdmin = dto.IsAdmin
            };

            _context.OrganizationMembers.Add(organizationMember);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = organizationMember.MemberId }, new OrganizationMemberDto
            {
                MemberId = organizationMember.MemberId,
                OrganizationId = organizationMember.OrganizationId,
                UserId = organizationMember.UserId,
                JoinDate = organizationMember.JoinDate,
                IsAdmin = organizationMember.IsAdmin
            });
        }

        // PUT: api/organizationmembers/5?userId=1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrganizationMemberDto dto, [FromQuery] int userId)
        {
            if (!await IsAdmin(userId))
                return Forbid("Sadece admin kullanıcılar güncelleme yapabilir.");

            var organizationMember = await _context.OrganizationMembers.FindAsync(id);
            if (organizationMember == null)
                return NotFound();

            // Only allow updating IsAdmin status, not changing organization or user
            organizationMember.IsAdmin = dto.IsAdmin;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/organizationmembers/5?userId=1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] int userId)
        {
            if (!await IsAdmin(userId))
                return Forbid("Sadece admin kullanıcılar silebilir.");

            var member = await _context.OrganizationMembers.FindAsync(id);
            if (member == null)
                return NotFound();

            _context.OrganizationMembers.Remove(member);
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
