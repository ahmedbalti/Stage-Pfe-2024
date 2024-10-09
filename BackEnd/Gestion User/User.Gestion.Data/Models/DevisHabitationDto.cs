#pragma warning disable CS8618 // Suppress "Non-nullable property must contain a non-null value" warnings


using System;

namespace User.Gestion.Data.Models
{
    public class DevisHabitationDto : DevisDto
    {
        public string Adresse { get; set; }
        public int Surface { get; set; }
        public int NombreDePieces { get; set; }
    }
}
#pragma warning restore CS8618
