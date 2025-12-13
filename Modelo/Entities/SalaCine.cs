using System;
using System.Collections.Generic;

namespace Modelo.Entities;

public partial class SalaCine
{
    public int IdSala { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Eliminado { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<PeliculaSalacine> PeliculaSalacines { get; set; } = new List<PeliculaSalacine>();
}
