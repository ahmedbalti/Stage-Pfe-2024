#pragma warning disable CS8618 // Suppress "Non-nullable property must contain a non-null value" warnings


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
#pragma warning restore CS8618
