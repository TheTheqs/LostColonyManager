using LostColonyManager.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace LostColonyManager.Interface.Controllers
{
    [ApiController]
    [Route("api/planets")]
    public sealed class PlanetsController : ControllerBase
    {
        private readonly RegisterPlanetUseCase _registerUseCase;
        private readonly DeletePlanetUseCase _deleteUseCase;
        private readonly GetPlanetUseCase _getUseCase;

        public PlanetsController(
            RegisterPlanetUseCase registerUseCase,
            DeletePlanetUseCase deleteUseCase,
            GetPlanetUseCase getPlanetUse
        )
        {
            _registerUseCase = registerUseCase;
            _deleteUseCase = deleteUseCase;
            _getUseCase = getPlanetUse;
        }

        /// <summary>
        /// Registers a new Planet.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(RegisterPlanetResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegisterPlanetResponse>> RegisterAsync(
            [FromBody] RegisterPlanetRequest request
        )
        {
            var response = await _registerUseCase.ExecuteAsync(request);

            // 201 + body
            return StatusCode(StatusCodes.Status201Created, response);
        }

        /// <summary>
        /// Deletes an existing Planet by Name.
        /// </summary>
        [HttpDelete("{name}")]
        [ProducesResponseType(typeof(DeletePlanetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DeletePlanetResponse>> DeleteAsync(
            [FromRoute] string name
        )
        {
            var response = await _deleteUseCase.ExecuteAsync(
                new DeletePlanetRequest { Name = name }
            );

            return Ok(response);
        }
        /// <summary>
        /// Gets all planets.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(GetPlanetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetPlanetResponse>> GetAllAsync()
        {
            var response = await _getUseCase.ExecuteAsync(
                new GetPlanetRequest { }
            );

            return Ok(response);
        }
        /// <summary>
        /// Gets a planet by Id.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(GetPlanetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetPlanetResponse>> GetByIdAsync(
            [FromRoute] Guid id
        )
        {
            var response = await _getUseCase.ExecuteAsync(
                new GetPlanetRequest { Id = id }
            );

            return Ok(response);
        }
    }
}
