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
    }
}
