using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Gestion.Service.Services;
using User.Gestion.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_User.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SinistresController : ControllerBase
    {
        private readonly ISinistreService _sinistreService;

        public SinistresController(ISinistreService sinistreService)
        {
            _sinistreService = sinistreService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sinistre>>> GetSinistres()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _sinistreService.GetSinistresByUserIdAsync(userId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sinistre>> GetSinistre(int id)
        {
            var sinistre = await _sinistreService.GetSinistreByIdAsync(id);
            if (sinistre == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (sinistre.UserId != userId)
            {
                return Forbid();
            }

            return Ok(sinistre);
        }

        [HttpPost]
        public async Task<ActionResult<Sinistre>> PostSinistre(Sinistre sinistre)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            sinistre.UserId = userId;

            await _sinistreService.AddSinistreAsync(sinistre);
            return CreatedAtAction(nameof(GetSinistre), new { id = sinistre.Id }, sinistre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSinistre(int id, Sinistre sinistre)
        {
            if (id != sinistre.Id)
            {
                return BadRequest();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            sinistre.UserId = userId;

            try
            {
                await _sinistreService.UpdateSinistreAsync(sinistre);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _sinistreService.SinistreExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
    }
}
