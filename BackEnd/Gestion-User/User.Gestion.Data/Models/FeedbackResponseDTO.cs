#pragma warning disable CS8618 // Suppress "Non-nullable property must contain a non-null value" warnings


namespace User.Gestion.Service.Models.Feedback
{
    public class FeedbackResponseDTO
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? ProfileImage { get; set; } // Ajout de la propriété ProfileImage

    }
}
#pragma warning restore CS8618
