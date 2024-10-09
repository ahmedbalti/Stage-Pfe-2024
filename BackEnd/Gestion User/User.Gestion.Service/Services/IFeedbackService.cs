using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.Gestion.Data.Models;
using User.Gestion.Service.Models.Feedback;

namespace User.Gestion.Service.Services
{
    public interface IFeedbackService
    {
        Task<Feedback> AddFeedbackAsync(Feedback feedback);
        Task<IEnumerable<Feedback>> GetFeedbacksForUserAsync(string userId);
        Task<double> GetAverageRatingAsync();
        Task<IEnumerable<RatingDistributionDTO>> GetRatingDistributionAsync();
        Task<IEnumerable<Feedback>> GetAllFeedbacksAsync();
    }
}
