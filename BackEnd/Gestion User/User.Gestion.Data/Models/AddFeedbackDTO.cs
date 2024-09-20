using System.ComponentModel.DataAnnotations;

namespace User.Gestion.Service.Models.Feedback
{
    public class AddFeedbackDTO
    {
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string? Comment { get; set; }
    }
}
