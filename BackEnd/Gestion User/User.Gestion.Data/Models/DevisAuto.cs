using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Gestion.Data.Models
{
    public class DevisAuto : Devis
    {
        public string NumeroImmatriculation { get; set; }
        public int NombreDeChevaux { get; set; }
        public int AgeVoiture { get; set; }
        public string Carburant { get; set; } // Essence ou Diesel
    }
}
