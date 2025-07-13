using DayOff.Application.Features.Departments.Commands;
using DayOff.Application.Features.Departments.Queries;
using DayOff.Application.Interfaces.Repositories;
using DayOff.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DayOff.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentCommand command)
        {
            var departmentId = await _mediator.Send(command);
            return Ok(departmentId);
        }

        // Update
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDepartmentCommand command)
        {
            if (id != command.UpdateDto.DepartmentId)
                return BadRequest("Department ID mismatch.");

            var result = await _mediator.Send(command);
            if (!result) return NotFound("Department not found.");

            return NoContent();
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteDepartmentCommand(id));
            if (!result) return NotFound("Department not found.");

            return NoContent();
        }

        // Get All
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _mediator.Send(new GetAllDepartmentsQuery());
            return Ok(departments);
        }

        // Get By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _mediator.Send(new GetDepartmentByIdQuery(id));
            if (department == null) return NotFound();

            return Ok(department);
        }
    }
}
