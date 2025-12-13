namespace Application.Dtos.Pelicula;

public class PeliculaRequestDto
{
    public string Nombre { get; set; } = null!;
    public int? Duracion { get; set; }
    public int IdSalaCine { get; set; }
    public DateOnly FechaPublicacion { get; set; }
    public DateOnly? FechaFin { get; set; }
}
