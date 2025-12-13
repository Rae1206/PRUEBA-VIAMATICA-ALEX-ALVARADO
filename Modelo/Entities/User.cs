using System;

namespace Modelo.Entities;

public class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool Eliminado { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
