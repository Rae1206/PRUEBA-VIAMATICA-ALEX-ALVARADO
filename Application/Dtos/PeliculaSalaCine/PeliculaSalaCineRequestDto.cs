namespace Application.Dtos.PeliculaSalaCine;

public class PeliculaSalaCineRequestDto
{
    // Datos de la película
    public string NombrePelicula { get; set; } = null!;
    public int? Duracion { get; set; }

    // Datos de la relación
    public int IdSalaCine { get; set; }
    public DateOnly FechaPublicacion { get; set; }
    public DateOnly? FechaFin { get; set; }
}
