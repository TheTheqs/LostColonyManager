using LostColonyManager.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace LostColonyManager.Interface.Controllers
{
    [ApiController]
    [Route("api/races")]
    public sealed class RacesController : ControllerBase
    {
        private readonly RegisterRaceUseCase _registerUseCase;
        private readonly DeleteRaceUseCase _deleteUseCase;

        public RacesController(
            RegisterRaceUseCase registerUseCase,
            DeleteRaceUseCase deleteUseCase
        )
        {
            _registerUseCase = registerUseCase;
            _deleteUseCase = deleteUseCase;
        }

        /// <summary>
        /// Registers a new Race.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(RegisterRaceResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegisterRaceResponse>> RegisterAsync(
            [FromBody] RegisterRaceRequest request
        )
        {
            var response = await _registerUseCase.ExecuteAsync(request);

            // 201 + body
            return StatusCode(StatusCodes.Status201Created, response);
        }

        /// <summary>
        /// Deletes an existing Race by Name.
        /// </summary>
        [HttpDelete("{name}")]
        [ProducesResponseType(typeof(DeleteRaceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DeleteRaceResponse>> DeleteAsync(
            [FromRoute] string name
        )
        {
            var response = await _deleteUseCase.ExecuteAsync(
                new DeleteRaceRequest { Name = name }
            );

            return Ok(response);
        }
    }
}
