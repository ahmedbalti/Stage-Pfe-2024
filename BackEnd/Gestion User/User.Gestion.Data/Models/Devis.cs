using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace User.Gestion.Data.Models
{
    public class Devis
    {
        [Key]
        public int IdDevis { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TypeAssurance TypeAssurance { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Montant { get; set; }

        [Required]
        public string OwnerId { get; set; }

        // Navigation property
        [ForeignKey("OwnerId")]
        public ApplicationUser Owner { get; set; }

        public ICollection<Opportunity> Opportunities { get; set; }
    }
}

