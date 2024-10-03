
#pragma warning disable CS8618 // Suppress "Non-nullable property must contain a non-null value" warnings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Gestion.Data.Models
{
    public class SinistreDto
    {
        public string NumeroDossier { get; set; }
        public DateTime DateDeclaration { get; set; }
        public string Description { get; set; }
        public SinistreStatut Statut { get; set; }
        public decimal MontantEstime { get; set; }
        public decimal MontantPaye { get; set; }
    }

}
#pragma warning restore CS8618
