namespace Application.Dtos.Pelicula;

public class PeliculaResponseDto
{
    public int IdPelicula { get; set; }
    public string Nombre { get; set; } = null!;
    public int? Duracion { get; set; }
    public bool Eliminado { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
