using Gestion_User.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using User.Gestion.Data.Models;
using User.Gestion.Service.Models;
using User.Gestion.Service.Services;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Gestion_User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IEmailService _emailService;

        public TicketController(ITicketService ticketService, IEmailService emailService)
        {
            _ticketService = ticketService;
            _emailService = emailService;

        }

        private string GetUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new Exception("User ID not found in claims");
            }
            return userIdClaim.Value;
        }

        private bool IsUserInRole(string role)
        {
            return User.IsInRole(role);
        }


        [Authorize(Roles = "Client")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            var userId = GetUserId();
            var tickets = await _ticketService.GetTicketsByUserIdAsync(userId);
            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicketById(Guid id)
        {
            var userId = GetUserId();
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null || ticket.OwnerId != userId)
            {
                return NotFound();
            }
            return Ok(ticket);
        }


        [Authorize(Roles = "Client")]
        [HttpPost]
        public async Task<ActionResult<Ticket>> AddTicket([FromBody] TicketDTO ticketDTO)
        {
            if (ticketDTO == null)
            {
                return BadRequest("TicketDTO is null");
            }

            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is not found");
            }

            var ticket = new Ticket
            {
                Titre = ticketDTO.Titre,
                Description = ticketDTO.Description,
                Priority = ticketDTO.Priority,
                Statut = ticketDTO.Statut,
                OwnerId = userId,
                CreatedOn = DateTime.UtcNow
            };

            var newTicket = await _ticketService.AddTicketAsync(ticket);
            return CreatedAtAction(nameof(GetTicketById), new { id = newTicket.IdTicket }, newTicket);
        }


        [Authorize(Roles = "Client")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Ticket>> UpdateTicket(Guid id, [FromBody] TicketDTO ticketDTO)
        {
            if (ticketDTO == null)
            {
                return BadRequest("TicketDTO is null");
            }

            var userId = GetUserId();
            var existingTicket = await _ticketService.GetTicketByIdAsync(id);

            if (existingTicket == null || existingTicket.OwnerId != userId)
            {
                return NotFound();
            }

            existingTicket.Titre = ticketDTO.Titre;
            existingTicket.Description = ticketDTO.Description;
            existingTicket.Priority = ticketDTO.Priority;
            existingTicket.Statut = ticketDTO.Statut;
            existingTicket.ResolutionDate = ticketDTO.ResolutionDate;

            var updatedTicket = await _ticketService.UpdateTicketAsync(id, existingTicket);
            return Ok(updatedTicket);
        }

        [Authorize(Roles = "User")]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            return Ok(tickets);
        }

        [Authorize(Roles = "User")]
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Ticket>>> FilterTickets([FromQuery] TicketFilterDTO filter)
        {
            var tickets = await _ticketService.FilterTicketsAsync(filter);
            return Ok(tickets);
        }

        [Authorize(Roles = "User")]
        [HttpPut("{id}/respond")]
        public async Task<IActionResult> RespondToTicket(Guid id, [FromBody] TicketResponseDTO responseDTO)
        {
            var updatedTicket = await _ticketService.RespondToTicketAsync(id, responseDTO);
            if (updatedTicket == null)
            {
                return NotFound();
            }
            return Ok(updatedTicket);
        }


        [Authorize(Roles = "User")]
        [HttpPut("{ticketId}/status")]
        public async Task<IActionResult> UpdateTicketStatus(Guid ticketId, [FromBody] TicketStatusUpdateDTO statusUpdateDTO)
        {
            if (!Enum.TryParse<TicketStatut>(statusUpdateDTO.Statut, true, out var statut))
            {
                return BadRequest("Invalid status value.");
            }

            var success = await _ticketService.UpdateTicketStatus(ticketId, statut);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }




        [HttpGet("filterByTitle/{title}")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicketsByTitle(string title)
        {
            if (!Enum.TryParse(typeof(TicketTitle), title, true, out var parsedTitle))
            {
                return BadRequest("Invalid title value");
            }

            var userId = GetUserId();
            var tickets = await _ticketService.GetTicketsByTitleAndUserIdAsync((TicketTitle)parsedTitle, userId);
            return Ok(tickets);
        }

        [HttpGet("{ticketId}/responses")]
        public async Task<ActionResult<IEnumerable<TicketResponse>>> GetAllTicketResponses(Guid ticketId)
        {
            var responses = await _ticketService.GetAllTicketResponsesAsync(ticketId);
            return Ok(responses);
        }

        [Authorize(Roles = "User")]
        [HttpGet("statistics")]
        public async Task<ActionResult<TicketStatisticsDTO>> GetTicketStatistics()
        {
            var statistics = await _ticketService.GetTicketStatisticsAsync();
            return Ok(statistics);
        }



    }
}
