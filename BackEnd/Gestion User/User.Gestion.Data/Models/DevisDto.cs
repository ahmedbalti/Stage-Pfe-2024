using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Gestion.Data.Models
{
    public class DevisDto
    {
        public int IdDevis { get; set; }

        public TypeAssurance TypeAssurance { get; set; }
        public decimal Montant { get; set; }

    }
}
