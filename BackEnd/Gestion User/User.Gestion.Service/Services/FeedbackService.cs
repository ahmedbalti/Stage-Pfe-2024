using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.Gestion.Data.Models;
using User.Gestion.Service.Models.Feedback;

namespace User.Gestion.Service.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ApplicationDbContext _context;

        public FeedbackService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Feedback> AddFeedbackAsync(Feedback feedback)
        {
            if (feedback == null)
                throw new ArgumentNullException(nameof(feedback));

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
            return feedback;
        }

        public async Task<IEnumerable<Feedback>> GetFeedbacksForUserAsync(string userId)
        {
            return await _context.Feedbacks
                .Where(f => f.UserId == userId)
                .OrderByDescending(f => f.DateCreated)
                .ToListAsync();
        }

        public async Task<double> GetAverageRatingAsync()
        {
            var ratings = await _context.Feedbacks.Select(f => f.Rating).ToListAsync();
            if (ratings.Count == 0)
                return 0;

            return ratings.Average();
        }

        public async Task<IEnumerable<RatingDistributionDTO>> GetRatingDistributionAsync()
        {
            var distribution = await _context.Feedbacks
                .GroupBy(f => f.Rating)
                .Select(g => new RatingDistributionDTO
                {
                    Rating = g.Key,
                    Count = g.Count()
                })
                .OrderBy(d => d.Rating)
                .ToListAsync();

            return distribution;
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbacksAsync()
        {
            return await _context.Feedbacks
                .Include(f => f.User)
                .OrderByDescending(f => f.DateCreated)
                .ToListAsync();
        }
    }
}
