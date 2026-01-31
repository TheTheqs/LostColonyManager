using LostColonyManager.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace LostColonyManager.Interface.Controllers
{
    [ApiController]
    [Route("api/events")]
    public sealed class EventsController : ControllerBase
    {
        private readonly RegisterEventUseCase _registerUseCase;
        private readonly GetEventUseCase _getUseCase;

        public EventsController(RegisterEventUseCase registerUseCase, GetEventUseCase getUseCase)
        {
            _registerUseCase = registerUseCase;
            _getUseCase = getUseCase;
        }

        /// <summary>
        /// Registers a new Event.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(RegisterEventResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegisterEventResponse>> RegisterAsync(
            [FromBody] RegisterEventRequest request
        )
        {
            var response = await _registerUseCase.ExecuteAsync(request);

            // 201 + body
            return StatusCode(StatusCodes.Status201Created, response);
        }
        /// <summary>
        /// Gets all events.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(GetEventResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetEventResponse>> GetAllAsync()
        {
            var response = await _getUseCase.ExecuteAsync(
                new GetEventRequest { }
            );

            return Ok(response);
        }
        /// <summary>
        /// Gets a event by Id.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(GetEventResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetEventResponse>> GetByIdAsync(
            [FromRoute] Guid id
        )
        {
            var response = await _getUseCase.ExecuteAsync(
                new GetEventRequest { Id = id }
            );

            return Ok(response);
        }
    }
}
