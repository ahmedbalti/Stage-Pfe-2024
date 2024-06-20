using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using User.Gestion.Service.Services;
using User.Gestion.Data.Models;

namespace Gestion_User.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractsController(IContractService contractService)
        {
            _contractService = contractService;
        }

        private string GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim?.Value;
        }

        [HttpGet]
        public async Task<IActionResult> GetContracts()
        {
            string userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found or empty.");
            }

            var contracts = await _contractService.GetContractsByUserIdAsync(userId);
            return Ok(contracts);
        }

        [HttpPost("renew")]
        public async Task<IActionResult> RenewContract([FromBody] RenewalRequest request)
        {
            string userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found or empty.");
            }

            var success = await _contractService.RenewContractAsync(request.ContractId);
            if (!success)
            {
                return BadRequest("Unable to renew contract.");
            }
            return Ok("Contract renewed successfully.");
        }
    }
}
