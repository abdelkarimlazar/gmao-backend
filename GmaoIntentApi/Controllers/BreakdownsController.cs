using GmaoIntentApi.DTOs;
using GmaoIntentApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GmaoIntentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BreakdownsController : ControllerBase
    {
        private readonly IBreakdownService _breakdownService;

        public BreakdownsController(IBreakdownService breakdownService)
        {
            _breakdownService = breakdownService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BreakdownDto>>> GetBreakdowns()
        {
            return Ok(await _breakdownService.GetBreakdownsAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BreakdownDto>> GetBreakdown(int id)
        {
            var breakdown = await _breakdownService.GetBreakdownByIdAsync(id);

            if (breakdown is null)
            {
                return NotFound();
            }

            return Ok(breakdown);
        }

        [HttpPost]
        public async Task<ActionResult<BreakdownDto>> CreateBreakdown(CreateBreakdownDto dto)
        {
            var breakdown = await _breakdownService.CreateBreakdownAsync(dto);
            if (breakdown is null)
            {
                return BadRequest("Equipment or reported user not found.");
            }

            return CreatedAtAction(nameof(GetBreakdown), new { id = breakdown.Id }, breakdown);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBreakdown(int id, UpdateBreakdownDto dto)
        {
            var updated = await _breakdownService.UpdateBreakdownAsync(id, dto);
            if (updated == false)
            {
                return NotFound();
            }

            if (updated is null)
            {
                return BadRequest("Equipment or reported user not found.");
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBreakdown(int id)
        {
            var deleted = await _breakdownService.DeleteBreakdownAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
