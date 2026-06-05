using GmaoIntentApi.DTOs;
using GmaoIntentApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GmaoIntentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InterventionHistoryController : ControllerBase
    {
        private readonly IInterventionHistoryService _interventionHistoryService;

        public InterventionHistoryController(IInterventionHistoryService interventionHistoryService)
        {
            _interventionHistoryService = interventionHistoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InterventionHistoryDto>>> GetInterventionHistories()
        {
            return Ok(await _interventionHistoryService.GetInterventionHistoriesAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<InterventionHistoryDto>> GetInterventionHistory(int id)
        {
            var history = await _interventionHistoryService.GetInterventionHistoryByIdAsync(id);

            if (history is null)
            {
                return NotFound();
            }

            return Ok(history);
        }

        [HttpGet("task/{taskId:int}")]
        public async Task<ActionResult<IEnumerable<InterventionHistoryDto>>> GetInterventionHistoriesByTask(int taskId)
        {
            return Ok(await _interventionHistoryService.GetInterventionHistoriesByTaskAsync(taskId));
        }

        [HttpPost]
        public async Task<ActionResult<InterventionHistoryDto>> CreateInterventionHistory(CreateInterventionHistoryDto dto)
        {
            var history = await _interventionHistoryService.CreateInterventionHistoryAsync(dto);
            if (history is null)
            {
                return BadRequest("Task or user not found.");
            }

            return CreatedAtAction(nameof(GetInterventionHistory), new { id = history.Id }, history);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateInterventionHistory(int id, UpdateInterventionHistoryDto dto)
        {
            var updated = await _interventionHistoryService.UpdateInterventionHistoryAsync(id, dto);
            if (updated == false)
            {
                return NotFound();
            }

            if (updated is null)
            {
                return BadRequest("Task or user not found.");
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteInterventionHistory(int id)
        {
            var deleted = await _interventionHistoryService.DeleteInterventionHistoryAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
