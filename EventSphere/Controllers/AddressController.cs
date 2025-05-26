using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventSphere.Models;
using EventSphere.ViewModels;

namespace EventSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly EventSphereDbContext _context;

        public AddressController(EventSphereDbContext context)
        {
            _context = context;
        }

        // GET: api/address
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var addresses = await _context.Addresses
                .Select(a => new AddressDto
                {
                    AddressId = a.AddressId,
                    Street = a.Street,
                    City = a.City,
                    District = a.District,
                    PostalCode = a.PostalCode,
                    Country = a.Country
                })
                .ToListAsync();

            return Ok(addresses);
        }

        // GET: api/address/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null) return NotFound();

            var dto = new AddressDto
            {
                AddressId = address.AddressId,
                Street = address.Street,
                City = address.City,
                District = address.District,
                PostalCode = address.PostalCode,
                Country = address.Country
            };

            return Ok(dto);
        }

        // POST: api/address?userId=1
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] int userId, AddressDto dto)
        {
            if (!await IsAdmin(userId))
                return StatusCode(403, "Sadece admin kullanıcılar adres ekleyebilir.");

            var address = new Address
            {
                Street = dto.Street,
                City = dto.City,
                District = dto.District,
                PostalCode = dto.PostalCode,
                Country = dto.Country
            };

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            dto.AddressId = address.AddressId;
            return CreatedAtAction(nameof(GetById), new { id = address.AddressId }, dto);
        }

        // PUT: api/address/5?userId=1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromQuery] int userId, AddressDto dto)
        {
            if (id != dto.AddressId)
                return BadRequest();

            if (!await IsAdmin(userId))
                return StatusCode(403, "Sadece admin kullanıcılar adres güncelleyebilir.");

            var address = await _context.Addresses.FindAsync(id);
            if (address == null) return NotFound();

            address.Street = dto.Street;
            address.City = dto.City;
            address.District = dto.District;
            address.PostalCode = dto.PostalCode;
            address.Country = dto.Country;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/address/5?userId=1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] int userId)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
                return NotFound();

            if (!await IsAdmin(userId))
                return StatusCode(403, "Sadece admin kullanıcılar adres silebilir.");

            _context.Addresses.Remove(address);
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
