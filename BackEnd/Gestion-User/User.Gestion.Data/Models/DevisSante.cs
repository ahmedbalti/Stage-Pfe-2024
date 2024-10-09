#pragma warning disable CS8618 // Suppress "Non-nullable property must contain a non-null value" warnings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Gestion.Data.Models
{
    public class DevisSante : Devis
    {
        public string NumeroSecuriteSociale { get; set; }
        public int Age { get; set; } // Ajout de l'âge de l'assuré
        public string Sexe { get; set; } // Sexe de l'assuré
        public bool Fumeur { get; set; } // Statut de fumeur
    }
}

#pragma warning restore CS8618
