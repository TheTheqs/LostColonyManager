using LostColonyManager.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace LostColonyManager.Interface.Controllers
{
    [ApiController]
    [Route("api/structures")]
    public sealed class StructuresController : ControllerBase
    {
        private readonly RegisterStructureUseCase _registerUseCase;
        private readonly ManageAssociationUseCase _manageAssociationUseCase;
        private readonly GetStructureUseCase _getUseCase;

        public StructuresController(
            RegisterStructureUseCase registerUseCase,
            ManageAssociationUseCase manageAssociationUseCase,
            GetStructureUseCase getUseCase
        )
        {
            _registerUseCase = registerUseCase;
            _manageAssociationUseCase = manageAssociationUseCase;
            _getUseCase = getUseCase;
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
        /// <summary>
        /// Creates or removes an association between a Structure and a Planet.
        /// </summary>
        [HttpPost("{structureId:guid}/planets/{planetId:guid}")]
        [ProducesResponseType(typeof(ManageAssociationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ManageAssociationResponse>> ManagePlanetAssociationAsync(
            [FromRoute] Guid structureId,
            [FromRoute] Guid planetId,
            [FromQuery] bool isAssociated
        )
        {
            var request = new ManageAssociationRequest
            {
                StructureId = structureId,
                PlanetId = planetId,
                IsAssociated = isAssociated
            };

            var response = await _manageAssociationUseCase.ExecuteAsync(request);

            return Ok(response);
        }
        /// <summary>
        /// Gets all structures.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(GetStructureResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetStructureResponse>> GetAllAsync()
        {
            var response = await _getUseCase.ExecuteAsync(
                new GetStructureRequest { }
            );

            return Ok(response);
        }
        /// <summary>
        /// Gets a structure by Id.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(GetStructureResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetStructureResponse>> GetByIdAsync(
            [FromRoute] Guid id
        )
        {
            var response = await _getUseCase.ExecuteAsync(
                new GetStructureRequest { Id = id }
            );

            return Ok(response);
        }
    }
}
