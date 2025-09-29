using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.API.LogAttributes;
using TaskTracker.Application.Tasks.Commands;
using TaskTracker.Application.Tasks.Dto;
using TaskTracker.Application.Tasks.Queries;
using TaskTracker.Domain.Entities;

namespace TaskTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [LogAction]
    [LogException]
    public class TskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<TskDto[]>> Get([FromQuery] GetTskAllQuery request, CancellationToken token)
        {
            var result = await _mediator.Send(request, token);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TskDto>> Get([FromQuery] GetTskByIdQuery request, CancellationToken token)
        {
            var result = await _mediator.Send(request, token);

            return result != null ? Ok(result) : NotFound(request);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateTskCommand request, CancellationToken token)
        {
            var result = await _mediator.Send(request, token);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<TskDto>> Put([FromBody] UpdateTskCommand request, CancellationToken token)
        {
            var result = await _mediator.Send(request, token);

            return result != null ? Ok(result) : NotFound(request);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] DeleteTskCommand request, CancellationToken token)
        {
            var result = await _mediator.Send(request, token);

            return result ? NoContent() : NotFound(request);
        }
    }
}
