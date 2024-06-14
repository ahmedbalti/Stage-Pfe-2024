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

namespace Gestion_User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        //private string GetUserId()
        //{
        //    return User.FindFirstValue(ClaimTypes.NameIdentifier);
        //}

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


      
        private string GetUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new Exception("User ID not found in claims");
            }
            return userIdClaim.Value;
        }


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
    }
}
