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

