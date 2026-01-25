using LostColonyManager.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace LostColonyManager.Interface.Controllers
{
    [ApiController]
    [Route("api/structures")]
    public sealed class StructuresController : ControllerBase
    {
        private readonly RegisterStructureUseCase _registerUseCase;

        public StructuresController(
            RegisterStructureUseCase registerUseCase
        )
        {
            _registerUseCase = registerUseCase;
        }

        /// <summary>
        /// Registers a new Structure.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(RegisterStructureResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegisterStructureResponse>> RegisterAsync(
            [FromBody] RegisterStructureRequest request
        )
        {
            var response = await _registerUseCase.ExecuteAsync(request);

            // 201 + body
            return StatusCode(StatusCodes.Status201Created, response);
        }
    }
}
