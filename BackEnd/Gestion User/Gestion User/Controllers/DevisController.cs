using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using User.Gestion.Data.Models;
using User.Gestion.Service.Services;
using Microsoft.AspNetCore.Http;

namespace Gestion_User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DevisController : ControllerBase
    {
        private readonly IDevisService _devisService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DevisController(IDevisService devisService, IHttpContextAccessor httpContextAccessor)
        {
            _devisService = devisService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DevisDto>>> GetDevis()
        {
            var ownerId = GetOwnerIdFromToken();
            if (string.IsNullOrEmpty(ownerId))
            {
                return Unauthorized();
            }

            var devisList = await _devisService.GetDevisByOwnerId(ownerId);
            var devisDtoList = new List<DevisDto>();

            foreach (var devis in devisList)
            {
                devisDtoList.Add(MapDevisToDto(devis));
            }

            return Ok(devisDtoList);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<DevisDto>> GetDevisById(int id)
        {
            var ownerId = GetOwnerIdFromToken();
            if (string.IsNullOrEmpty(ownerId))
            {
                return Unauthorized();
            }

            var devis = await _devisService.GetDevisByIdAndOwnerId(id, ownerId);

            if (devis == null)
            {
                return NotFound();
            }

            return Ok(MapDevisToDto(devis));
        }

        [HttpPost("auto")]
        public async Task<ActionResult> CreateDevisAuto([FromBody] DevisAutoDto devisAutoDto)
        {
            var ownerId = GetOwnerIdFromToken();
            if (string.IsNullOrEmpty(ownerId))
            {
                return Unauthorized();
            }

            var devisAuto = new DevisAuto
            {
                TypeAssurance = TypeAssurance.Auto,
                NumeroImmatriculation = devisAutoDto.NumeroImmatriculation,
                NombreDeChevaux = devisAutoDto.NombreDeChevaux,
                AgeVoiture = devisAutoDto.AgeVoiture,
                Carburant = devisAutoDto.Carburant,
                OwnerId = ownerId
            };

            devisAuto.Montant = _devisService.CalculerMontant(devisAuto);

            var createdDevis = await _devisService.CreateDevis(devisAuto);
            return CreatedAtAction(nameof(GetDevisById), new { id = createdDevis.IdDevis }, MapDevisToDto(createdDevis));
        }


        //[HttpGet("{id}")]
        //public async Task<ActionResult<DevisDto>> GetDevisById(int id)
        //{
        //    var ownerId = GetOwnerIdFromToken();
        //    if (string.IsNullOrEmpty(ownerId))
        //    {
        //        return Unauthorized();
        //    }

        //    var devis = await _devisService.GetDevisByIdAndOwnerId(id, ownerId);

        //    if (devis == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(MapDevisToDto(devis));
        //}

        //[HttpPost("auto")]
        //public async Task<ActionResult> CreateDevisAuto([FromBody] DevisAutoDto devisAutoDto)
        //{
        //    var ownerId = GetOwnerIdFromToken();
        //    if (string.IsNullOrEmpty(ownerId))
        //    {
        //        return Unauthorized();
        //    }

        //    var devisAuto = new DevisAuto
        //    {
        //        TypeAssurance = TypeAssurance.Auto,
        //        NumeroImmatriculation = devisAutoDto.NumeroImmatriculation,
        //        NombreDeChevaux = devisAutoDto.NombreDeChevaux,
        //        AgeVoiture = devisAutoDto.AgeVoiture,
        //        Carburant = devisAutoDto.Carburant,
        //        OwnerId = ownerId // Assigner l'OwnerId ici
        //    };

        //    devisAuto.Montant = _devisService.CalculerMontant(devisAuto);

        //    var createdDevis = await _devisService.CreateDevis(devisAuto);
        //    return CreatedAtAction(nameof(GetDevisById), new { id = createdDevis.IdDevis }, MapDevisToDto(createdDevis));
        //}

        [HttpPost("sante")]
        public async Task<ActionResult> CreateDevisSante([FromBody] DevisSanteDto devisSanteDto)
        {
            var ownerId = GetOwnerIdFromToken();
            if (string.IsNullOrEmpty(ownerId))
            {
                return Unauthorized();
            }

            var devisSante = new DevisSante
            {
                TypeAssurance = TypeAssurance.Sante,
                NumeroSecuriteSociale = devisSanteDto.NumeroSecuriteSociale,
                Age = devisSanteDto.Age,
                Sexe = devisSanteDto.Sexe,
                Fumeur = devisSanteDto.Fumeur,
                OwnerId = ownerId // Assigner l'OwnerId ici
            };

            devisSante.Montant = _devisService.CalculerMontant(devisSante);

            var createdDevis = await _devisService.CreateDevis(devisSante);
            return CreatedAtAction(nameof(GetDevisById), new { id = createdDevis.IdDevis }, MapDevisToDto(createdDevis));
        }

        [HttpPost("habitation")]
        public async Task<ActionResult> CreateDevisHabitation([FromBody] DevisHabitationDto devisHabitationDto)
        {
            var ownerId = GetOwnerIdFromToken();
            if (string.IsNullOrEmpty(ownerId))
            {
                return Unauthorized();
            }

            var devisHabitation = new DevisHabitation
            {
                TypeAssurance = TypeAssurance.Habitation,
                Adresse = devisHabitationDto.Adresse,
                Surface = devisHabitationDto.Surface,
                NombreDePieces = devisHabitationDto.NombreDePieces,
                OwnerId = ownerId // Assigner l'OwnerId ici
            };

            devisHabitation.Montant = _devisService.CalculerMontant(devisHabitation);

            var createdDevis = await _devisService.CreateDevis(devisHabitation);
            return CreatedAtAction(nameof(GetDevisById), new { id = createdDevis.IdDevis }, MapDevisToDto(createdDevis));
        }

        [HttpPost("vie")]
        public async Task<ActionResult> CreateDevisVie([FromBody] DevisVieDto devisVieDto)
        {
            var ownerId = GetOwnerIdFromToken();
            if (string.IsNullOrEmpty(ownerId))
            {
                return Unauthorized();
            }

            var devisVie = new DevisVie
            {
                TypeAssurance = TypeAssurance.Vie,
                Beneficiaire = devisVieDto.Beneficiaire,
                Duree = devisVieDto.Duree,
                Capital = devisVieDto.Capital,
                OwnerId = ownerId // Assigner l'OwnerId ici
            };

            devisVie.Montant = _devisService.CalculerMontant(devisVie);

            var createdDevis = await _devisService.CreateDevis(devisVie);
            return CreatedAtAction(nameof(GetDevisById), new { id = createdDevis.IdDevis }, MapDevisToDto(createdDevis));
        }

        //private DevisDto MapDevisToDto(Devis devis)
        //{
        //    return new DevisDto
        //    {
        //        IdDevis = devis.IdDevis,
        //        TypeAssurance = devis.TypeAssurance,
        //        Montant = devis.Montant
        //    };
        //}

        private DevisDto MapDevisToDto(Devis devis)
        {
            switch (devis)
            {
                case DevisAuto auto:
                    return new DevisAutoDto
                    {
                        IdDevis = auto.IdDevis,
                        TypeAssurance = auto.TypeAssurance,
                        Montant = auto.Montant,
                        NumeroImmatriculation = auto.NumeroImmatriculation,
                        NombreDeChevaux = auto.NombreDeChevaux,
                        AgeVoiture = auto.AgeVoiture,
                        Carburant = auto.Carburant
                    };
                case DevisSante sante:
                    return new DevisSanteDto
                    {
                        IdDevis = sante.IdDevis,
                        TypeAssurance = sante.TypeAssurance,
                        Montant = sante.Montant,
                        NumeroSecuriteSociale = sante.NumeroSecuriteSociale,
                        Age = sante.Age,
                        Sexe = sante.Sexe,
                        Fumeur = sante.Fumeur
                    };
                case DevisHabitation habitation:
                    return new DevisHabitationDto
                    {
                        IdDevis = habitation.IdDevis,
                        TypeAssurance = habitation.TypeAssurance,
                        Montant = habitation.Montant,
                        Adresse = habitation.Adresse,
                        Surface = habitation.Surface,
                        NombreDePieces = habitation.NombreDePieces
                    };
                case DevisVie vie:
                    return new DevisVieDto
                    {
                        IdDevis = vie.IdDevis,
                        TypeAssurance = vie.TypeAssurance,
                        Montant = vie.Montant,
                        Beneficiaire = vie.Beneficiaire,
                        Duree = vie.Duree,
                        Capital = vie.Capital
                    };
                default:
                    return new DevisDto
                    {
                        IdDevis = devis.IdDevis,
                        TypeAssurance = devis.TypeAssurance,
                        Montant = devis.Montant
                    };
            }
        }



        private string GetOwnerIdFromToken()
        {
            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var ownerIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return ownerIdClaim;
        }

    }
}

