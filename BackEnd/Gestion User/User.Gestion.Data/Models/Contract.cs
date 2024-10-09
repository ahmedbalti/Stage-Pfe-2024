#pragma warning disable CS8618 // Suppress "Non-nullable property must contain a non-null value" warnings


using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace User.Gestion.Data.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        public string UserId { get; set; }  // Foreign key
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }  // Navigation property

        public string ClientId { get; set; }  // Foreign key for Client
        [ForeignKey("ClientId")]
        public ApplicationUser Client { get; set; }  // Navigation property for Client

        // New property to hold the client's name
        [NotMapped]
        public string ClientName { get; set; }
    }
}
#pragma warning restore CS8618
