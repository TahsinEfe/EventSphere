using DayOff.Application.Features.WeeklyDayOffStats.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DayOff.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("weekly-day-off")]
        public async Task<IActionResult> GetWeeklyDayOffStats()
        {
            var result = await _mediator.Send(new GetWeeklyDayOffStatsQuery());
            return Ok(result);
        }
    }
}
