using LostColonyManager.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace LostColonyManager.Interface.Controllers
{
    [ApiController]
    [Route("api/exports")]
    public sealed class ExportsController : ControllerBase
    {
        private readonly ExportDatabaseUseCase _exportDatabaseUseCase;

        public ExportsController(
            ExportDatabaseUseCase exportDatabaseUseCase
        )
        {
            _exportDatabaseUseCase = exportDatabaseUseCase;
        }

        /// <summary>
        /// Exports a full database snapshot as JSON (flat, relationship by IDs only).
        /// </summary>
        [HttpGet("database")]
        [ProducesResponseType(typeof(ExportDatabaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ExportDatabaseResponse>> ExportDatabaseAsync()
        {
            var response = await _exportDatabaseUseCase.ExecuteAsync(
                new ExportDatabaseRequest()
            );

            return Ok(response);
        }
    }
}