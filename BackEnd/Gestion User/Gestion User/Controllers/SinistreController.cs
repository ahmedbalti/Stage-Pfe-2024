using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Crypto;
using User.Gestion.Data.Models;
using User.Gestion.Service.Services;
using AutoMapper;

namespace User.Gestion.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SinistresController : ControllerBase
    {
        private readonly ISinistreService _sinistreService;
        private readonly ILogger<SinistresController> _logger;
        private readonly IMapper _mapper;

        public SinistresController(ISinistreService sinistreService, ILogger<SinistresController> logger, IMapper mapper)
        {
            _sinistreService = sinistreService;
            _logger = logger;
            _mapper = mapper;
        }



        [Authorize(Roles = "Client")]
        [HttpPost]
        public async Task<IActionResult> CreateSinistre([FromBody] SinistreDto sinistreDto)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            if (!Enum.TryParse<SinistreStatut>(sinistreDto.Statut.ToString(), true, out var statut))
            {
                return BadRequest("Invalid status value");
            }

            var sinistre = _mapper.Map<Sinistre>(sinistreDto);
            sinistre.UserId = userId;
            sinistre.DateDeclaration = DateTime.UtcNow; // Date actuelle
            sinistre.Statut = SinistreStatut.Ouverte; // Statut initial

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdSinistre = await _sinistreService.CreateSinistre(sinistre);
            var createdSinistreDto = _mapper.Map<SinistreDto>(createdSinistre);

            return CreatedAtAction(nameof(GetSinistreById), new { id = createdSinistre.Id }, createdSinistreDto);
        }

        private string GetUserId()
        {
            var userId = User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            _logger.LogInformation($"User ID from token: {userId}");
            return userId;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSinistre(int id, [FromBody] SinistreDto sinistreDto)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var existingSinistre = await _sinistreService.GetSinistreById(id, userId);
            if (existingSinistre == null)
            {
                return NotFound();
            }

            _logger.LogInformation($"Updating sinistre ID: {id} for user ID: {userId}");

            // Mise à jour des propriétés avec les valeurs de sinistreDto
            existingSinistre.NumeroDossier = sinistreDto.NumeroDossier;
            existingSinistre.DateDeclaration = sinistreDto.DateDeclaration;
            existingSinistre.Description = sinistreDto.Description;
            existingSinistre.Statut = sinistreDto.Statut;
            existingSinistre.MontantEstime = sinistreDto.MontantEstime;
            existingSinistre.MontantPaye = sinistreDto.MontantPaye;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _sinistreService.UpdateSinistre(existingSinistre);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSinistreById(int id)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var sinistre = await _sinistreService.GetSinistreById(id, userId);
            if (sinistre == null)
            {
                return NotFound();
            }
            return Ok(sinistre);
        }

        [HttpGet]
        public async Task<IActionResult> GetSinistresByUser()
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var sinistres = await _sinistreService.GetSinistresByUser(userId);
            return Ok(sinistres);
        }
    }
}
