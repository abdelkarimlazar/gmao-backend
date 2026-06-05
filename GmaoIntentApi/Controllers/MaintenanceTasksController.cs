using GmaoIntentApi.DTOs;
using GmaoIntentApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GmaoIntentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MaintenanceTasksController : ControllerBase
    {
        private readonly IMaintenanceTaskService _maintenanceTaskService;

        public MaintenanceTasksController(IMaintenanceTaskService maintenanceTaskService)
        {
            _maintenanceTaskService = maintenanceTaskService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResultDto<MaintenanceTaskDto>>> GetMaintenanceTasks([FromQuery] MaintenanceTaskQueryDto query)
        {
            return Ok(await _maintenanceTaskService.GetMaintenanceTasksAsync(query));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MaintenanceTaskDto>> GetMaintenanceTask(int id)
        {
            var task = await _maintenanceTaskService.GetMaintenanceTaskByIdAsync(id);

            if (task is null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<MaintenanceTaskDto>> CreateMaintenanceTask(CreateMaintenanceTaskDto dto)
        {
            var task = await _maintenanceTaskService.CreateMaintenanceTaskAsync(dto);
            if (task is null)
            {
                return BadRequest("Equipment or assigned user not found.");
            }

            return CreatedAtAction(nameof(GetMaintenanceTask), new { id = task.Id }, task);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMaintenanceTask(int id, UpdateMaintenanceTaskDto dto)
        {
            var updated = await _maintenanceTaskService.UpdateMaintenanceTaskAsync(id, dto);
            if (updated == false)
            {
                return NotFound();
            }

            if (updated is null)
            {
                return BadRequest("Equipment or assigned user not found.");
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMaintenanceTask(int id)
        {
            var deleted = await _maintenanceTaskService.DeleteMaintenanceTaskAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
