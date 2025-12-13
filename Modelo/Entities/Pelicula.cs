using System;
using System.Collections.Generic;

namespace Modelo.Entities;

public partial class Pelicula
{
    public int IdPelicula { get; set; }

    public string Nombre { get; set; } = null!;

    public int? Duracion { get; set; }

    public bool Eliminado { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<PeliculaSalacine> PeliculaSalacines { get; set; } = new List<PeliculaSalacine>();
}
