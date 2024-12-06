using ExampleWebApplication.Commands;
using ExampleWebApplication.Dtos;
using ExampleWebApplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExampleWebApplication.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ExampleController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExampleDto>>> GetAll([FromQuery] GetAllDto getAllDto)
        {
            return Ok(await mediator.Send(new GetAllQuery
            {
                OrderBy = getAllDto.OrderBy,
            }));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ExampleDto?>> GetById([FromRoute] int id)
        {
            var getByIdQuery = await mediator.Send(new GetByIdQuery
            {
                Id = id,
            });

            if (getByIdQuery.IsFailed) return BadRequest(getByIdQuery.Errors.Select(x => x.Message));
            if (getByIdQuery.Value is null) return NotFound();

            return Ok(getByIdQuery.Value);
        }

        [HttpPost]
        public async Task<ActionResult<ExampleDto?>> Create([FromQuery] CreateExampleDto createExampleDto)
        {
            var createCommand = await mediator.Send(new CreateCommand
            {
                Name = createExampleDto.Name,
                Description = createExampleDto.Description,
                Visible = createExampleDto.Visible,
            });

            if (createCommand.IsFailed) return BadRequest(createCommand.Errors.Select(x => x.Message));
            if (createCommand.Value is null) return BadRequest();

            return Ok(createCommand.Value);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ExampleDto?>> Update([FromRoute] int id, [FromQuery] UpdateExampleDto updateExampleDto)
        {
            var updateCommand = await mediator.Send(new UpdateCommand
            {
                Id = id,
                Name = updateExampleDto.Name,
                Description = updateExampleDto.Description,
                Visible = updateExampleDto.Visible,
            });

            if (updateCommand.IsFailed) return BadRequest(updateCommand.Errors.Select(x => x.Message));
            if (updateCommand.Value is null) return NotFound();

            return Ok(updateCommand.Value);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ExampleDto?>> Delete([FromRoute] int id)
        {
            var deleteCommand = await mediator.Send(new DeleteCommand
            {
                Id = id,
            });

            if (deleteCommand.IsFailed) return BadRequest(deleteCommand.Errors.Select(x => x.Message));
            if (deleteCommand.Value is null) return NotFound();

            return Ok(deleteCommand.Value);
        }
    }
}