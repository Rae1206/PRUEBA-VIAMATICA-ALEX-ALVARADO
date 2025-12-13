namespace Application.Dtos.SalaCine;

public class SalaCineResponseDto
{
    public int IdSala { get; set; }
    public string Nombre { get; set; } = null!;
    public bool Eliminado { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
