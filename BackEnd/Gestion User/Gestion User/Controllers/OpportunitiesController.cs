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
    public class OpportunitiesController : ControllerBase
    {
        private readonly IOpportunityService _opportunityService;
        private readonly IDevisService _devisService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OpportunitiesController(IOpportunityService opportunityService, IDevisService devisService, IHttpContextAccessor httpContextAccessor)
        {
            _opportunityService = opportunityService;
            _devisService = devisService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Opportunity>>> GetOpportunities()
        {
            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var opportunities = await _opportunityService.GetOpportunitiesByUserId(userId);
            return Ok(opportunities);
        }

        [HttpPost("approve/{id}")]
        public async Task<ActionResult> ApproveOpportunity(int id)
        {
            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            await _opportunityService.ApproveOpportunity(id, userId);
            return NoContent();
        }

        private string GetUserIdFromToken()
        {
            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userIdClaim;
        }
    }
}
