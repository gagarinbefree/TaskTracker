using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Tasks.Commands;
using TaskTracker.Application.Tasks.Queries;
using TaskTracker.Domain.Entities;

namespace TaskTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Tsk[]>> Get([FromQuery] GetTskAllQuery request, CancellationToken token)
        {
            IEnumerable<Tsk> result = await _mediator.Send(request, token);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tsk[]>> Get([FromQuery] GetTskByIdQuery request, CancellationToken token)
        {
            Tsk? result = await _mediator.Send(request, token);

            return result != null ? Ok(result) : NotFound(request);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateTskCommand request, CancellationToken token)
        {
            int id = await _mediator.Send(request, token);

            return Ok(id);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] DeleteTskCommand request, CancellationToken token)
        {
            await _mediator.Send(request, token);

            return NoContent();
        }
    }
}
