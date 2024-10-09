#pragma warning disable CS8618 // Suppress "Non-nullable property must contain a non-null value" warnings


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Gestion.Data.Models
{
    public class DevisHabitation : Devis
    {
        public string InformationsHabitation { get; set; }
        public string Adresse { get; set; }
        public int Surface { get; set; }
        public int NombreDePieces { get; set; }
    }
}

#pragma warning restore CS8618
