#pragma warning disable CS8618 // Suppress "Non-nullable property must contain a non-null value" warnings

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
#pragma warning restore CS8618
