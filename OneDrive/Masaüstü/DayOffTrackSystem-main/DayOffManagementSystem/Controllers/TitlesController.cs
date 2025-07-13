using DayOff.Application.Features.Titles.Commands;
using DayOff.Application.Features.Titles.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DayOff.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TitlesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TitlesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTitleCommand command)
        {
            var titleId = await _mediator.Send(command);
            return Ok(titleId);
        }

        // Update
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTitleCommand command)
        {
            if (id != command.TitleId)
                return BadRequest("Title ID mismatch.");

            var result = await _mediator.Send(command);
            if (!result) return NotFound("Title not found.");

            return NoContent();
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteTitleCommand(id));
            if (!result) return NotFound("Title not found.");
            return NoContent();
        }

        //Get All
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var titles = await _mediator.Send(new GetAllTitlesQuery());
            return Ok(titles);
        }

        // Get By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var title = await _mediator.Send(new GetTitleByIdQuery(id));
            if (title == null) return NotFound();

            return Ok(title);
        }
    }
    }
