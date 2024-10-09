#pragma warning disable CS8618 // Suppress "Non-nullable property must contain a non-null value" warnings


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Gestion.Data.Models
{
    public class DevisVie : Devis
    {
        public string Beneficiaire { get; set; }
        public int Duree { get; set; } // Durée en années
        public decimal Capital { get; set; } // Capital assuré
    }
}

#pragma warning restore CS8618
