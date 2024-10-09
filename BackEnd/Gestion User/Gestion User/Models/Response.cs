namespace Gestion_User.Models
{
    public class Response
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public bool? IsSuccess { get; set; }

    }

    public class LoginF2AModel
    {
        public string code { get; set; }
        public string userName { get; set; }

    }
}
