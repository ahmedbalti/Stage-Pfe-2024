namespace User.Gestion.Service.Models.Feedback
{
    public class FeedbackResponseDTO
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; }
        public string? ProfileImage { get; set; } // Ajout de la propriété ProfileImage

    }
}
