using LostColonyManager.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace LostColonyManager.Interface.Controllers
{
    [ApiController]
    [Route("api/campaigns")]
    public sealed class CampaignsController : ControllerBase
    {
        private readonly RegisterCampaignUseCase _registerUseCase;
        private readonly DeleteCampaignUseCase _deleteUseCase;
        
        public CampaignsController(
            RegisterCampaignUseCase registerUseCase,
            DeleteCampaignUseCase deleteUseCase
        )
        {
            _registerUseCase = registerUseCase;
            _deleteUseCase = deleteUseCase;
        }

        /// <summary>
        /// Registers a new Campaign.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(RegisterCampaignResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegisterCampaignResponse>> RegisterAsync(
            [FromBody] RegisterCampaignRequest request
        )
        {
            var response = await _registerUseCase.ExecuteAsync(request);

            // 201 + body
            return StatusCode(StatusCodes.Status201Created, response);
        }

        /// <summary>
        /// Deletes an existing Campaign by Name.
        /// </summary>
        [HttpDelete("{name}")]
        [ProducesResponseType(typeof(DeleteCampaignResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DeleteCampaignResponse>> DeleteAsync(
            [FromRoute] string name
        )
        {
            var response = await _deleteUseCase.ExecuteAsync(
                new DeleteCampaignRequest { Name = name }
            );

            return Ok(response);
        }
    }
}
