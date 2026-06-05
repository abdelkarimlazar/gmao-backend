using GmaoIntentApi.DTOs;
using GmaoIntentApi.Enums;
using GmaoIntentApi.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GmaoIntentApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentsController : ControllerBase
    {
        private readonly IEquipmentService
            _equipmentService;

        public EquipmentsController(
            IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        // Admin + Manager

        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetPaged(
            [FromQuery] EquipmentStatus? status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var result =
                await _equipmentService
                    .GetPagedAsync(
                        status,
                        pageNumber,
                        pageSize);

            return Ok(new
            {
                result.TotalCount,
                result.Items
            });
        }

        // Admin + Manager

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetById(
            int id)
        {
            var equipment =
                await _equipmentService
                    .GetByIdAsync(id);

            if (equipment == null)
            {
                return NotFound();
            }

            return Ok(equipment);
        }

        // Admin only

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(
            CreateEquipmentDto dto)
        {
            var created =
                await _equipmentService
                    .CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created);
        }

        // Admin + Technician

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> Update(
            int id,
            UpdateEquipmentDto dto)
        {
            await _equipmentService
                .UpdateAsync(id, dto);

            return NoContent();
        }

        // Admin only

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(
            int id)
        {
            await _equipmentService
                .DeleteAsync(id);

            return NoContent();
        }
    }
}