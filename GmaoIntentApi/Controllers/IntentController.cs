using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using GmaoIntentApi.Services;
using GmaoIntentApi.Data;

namespace GmaoIntentApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class IntentController : ControllerBase
    {
        private readonly IntentService _intentService;
        private readonly AppDbContext _context;

        public IntentController(IntentService intentService, AppDbContext context)
        {
            _intentService = intentService;
            _context = context;
        }

        //  SYNC
        [HttpGet("sync")]
        public async Task<IActionResult> Sync()
        {
            var demandes = await _intentService.GetNewDemandesAsync();
            return Ok(demandes);
        }

        //  GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var demandes = await _context.IntentDemandes.ToListAsync();
            return Ok(demandes);
        }

        //  ACCEPT
        [HttpPost("{id}/accept")]
        public async Task<IActionResult> Accept(int id)
        {
            var user = User.Identity?.Name ?? "Unknown";

            var result = await _intentService.AcceptDemande(id, user);

            if (!result)
                return BadRequest("Demande déjà traitée ou inexistante");

            return Ok(new
            {
                message = "Demande acceptée",
                by = user
            });
        }

        //  REFUSE
        [HttpPost("{id}/refuse")]
        public async Task<IActionResult> Refuse(int id, [FromBody] RefuseRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Motif))
                return BadRequest("Motif obligatoire");

            var user = User.Identity?.Name ?? "Unknown";

            var result = await _intentService.RefuseDemande(id, request.Motif, user);

            if (!result)
                return BadRequest("Demande déjà traitée ou inexistante");

            return Ok(new
            {
                message = "Demande refusée",
                by = user
            });
        }

        //  HISTORIQUE
        [HttpGet("{id}/historique")]
        public async Task<IActionResult> GetHistorique(int id)
        {
            var historique = await _intentService.GetHistorique(id);
            return Ok(historique);
        }
    }

    // DTO 
    public class RefuseRequest
    {
        public string Motif { get; set; } = string.Empty;
    }
}