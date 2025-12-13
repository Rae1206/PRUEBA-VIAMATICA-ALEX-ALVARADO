using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Dtos.SalaCine;

public class SalaDisponibilidadDto
{
    [Column("id_sala")]
    public int IdSala { get; set; }

    [Column("nombre_sala")]
    public string NombreSala { get; set; } = null!;

    [Column("total_peliculas")]
    public int TotalPeliculas { get; set; }

    [Column("mensaje")]
    public string Mensaje { get; set; } = null!;
}
