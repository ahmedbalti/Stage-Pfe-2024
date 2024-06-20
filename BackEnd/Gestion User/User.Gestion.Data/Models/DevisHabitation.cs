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

