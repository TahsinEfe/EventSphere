using DayOff.Application.Features.DayOffTypes.Queries;
using DayOff.Application.Interfaces;
using DayOff.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DayOff.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DayOffTypesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly DayOffDbContext _context;

        public DayOffTypesController(IMediator mediator, DayOffDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        

        [HttpGet("with-gender")]
        public async Task<IActionResult> GetWithGender()
        {
            var data = await _context.VwDayOffTypesWithGender.ToListAsync();
            return Ok(data);
        }

        [HttpGet("allowed-by-gender/{genderId}")]
        public async Task<IActionResult> GetAllowedByGender(decimal genderId)
        {
            var data = await _context.VwDayOffTypesWithGender
                .Where(x => x.IsGenderSpecific == false || (x.IsGenderSpecific == true && x.AllowedGenderId == genderId))
                .ToListAsync();

            return Ok(data);
        }

    }
}
