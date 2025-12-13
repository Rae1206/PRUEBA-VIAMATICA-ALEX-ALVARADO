using System;
using System.Collections.Generic;

namespace Modelo.Entities;

public partial class PeliculaSalacine
{
    public int IdPeliculaSala { get; set; }

    public int IdSalaCine { get; set; }

    public DateOnly FechaPublicacion { get; set; }

    public DateOnly? FechaFin { get; set; }

    public int IdPelicula { get; set; }

    public bool Eliminado { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Pelicula IdPeliculaNavigation { get; set; } = null!;

    public virtual SalaCine IdSalaCineNavigation { get; set; } = null!;
}
