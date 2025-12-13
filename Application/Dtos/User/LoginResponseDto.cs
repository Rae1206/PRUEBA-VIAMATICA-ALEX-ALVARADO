namespace Application.Dtos.User;

public class LoginResponseDto
{
    public string Token { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
}
