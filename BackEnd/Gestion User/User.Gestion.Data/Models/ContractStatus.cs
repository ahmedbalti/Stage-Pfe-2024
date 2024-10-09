#pragma warning disable CS8618 // Suppress "Non-nullable property must contain a non-null value" warnings

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace User.Gestion.Data.Models
{
    public enum ContractStatus
    {
        Inactive,
        Active
    }
}

#pragma warning restore CS8618
