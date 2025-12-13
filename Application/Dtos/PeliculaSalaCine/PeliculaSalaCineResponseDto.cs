namespace Application.Dtos.PeliculaSalaCine;

public class PeliculaSalaCineResponseDto
{
    public int IdPeliculaSala { get; set; }
    public int IdSalaCine { get; set; }
    public int IdPelicula { get; set; }
    public string NombrePelicula { get; set; } = null!;
    public string NombreSala { get; set; } = null!;
    public DateOnly FechaPublicacion { get; set; }
    public DateOnly? FechaFin { get; set; }
    public bool Eliminado { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
