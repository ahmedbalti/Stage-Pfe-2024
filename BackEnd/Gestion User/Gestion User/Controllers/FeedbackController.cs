using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using User.Gestion.Data.Models;
using User.Gestion.Service.Models.Feedback;
using User.Gestion.Service.Services;
using Microsoft.AspNetCore.Http;

namespace Gestion_User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FeedbackController(
            IFeedbackService feedbackService,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _feedbackService = feedbackService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetUserIdFromToken()
        {
            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userIdClaim;
        }

        // Endpoint pour les Clients de soumettre un feedback
        [Authorize(Roles = "Client")]
        [HttpPost]
        public async Task<IActionResult> AddFeedback([FromBody] AddFeedbackDTO feedbackDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var feedback = new Feedback
            {
                Id = Guid.NewGuid(),
                Rating = feedbackDTO.Rating,
                Comment = feedbackDTO.Comment,
                DateCreated = DateTime.UtcNow,
                UserId = userId
            };

            await _feedbackService.AddFeedbackAsync(feedback);
            return Ok(new { message = "Feedback ajouté avec succès" });
        }

        // Endpoint pour les Users de voir la moyenne des ratings
        [Authorize(Roles = "User")]
        [HttpGet("average-rating")]
        public async Task<ActionResult<AverageRatingDTO>> GetAverageRating()
        {
            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var average = await _feedbackService.GetAverageRatingAsync();
            return Ok(new AverageRatingDTO { AverageRating = average });
        }

        // Endpoint pour voir la distribution des ratings
        [Authorize(Roles = "User")]
        [HttpGet("rating-distribution")]
        public async Task<ActionResult<IEnumerable<RatingDistributionDTO>>> GetRatingDistribution()
        {
            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var distribution = await _feedbackService.GetRatingDistributionAsync();
            return Ok(distribution);
        }

        // Endpoint pour voir tous les commentaires des Clients
        [Authorize(Roles = "User")]
        [HttpGet("comments")]
        public async Task<ActionResult<IEnumerable<FeedbackResponseDTO>>> GetAllComments()
        {
            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var feedbacks = await _feedbackService.GetAllFeedbacksAsync();
            var feedbackDTOs = feedbacks.Select(f => new FeedbackResponseDTO
            {
                Id = f.Id,
                Rating = f.Rating,
                Comment = f.Comment,
                DateCreated = f.DateCreated,
                UserName = f.User.UserName,
                ProfileImage = f.User.ProfileImage // Assurez-vous que ceci récupère l'URL de l'image du profil
            }).ToList();

            return Ok(feedbackDTOs);
        }

    }
}
