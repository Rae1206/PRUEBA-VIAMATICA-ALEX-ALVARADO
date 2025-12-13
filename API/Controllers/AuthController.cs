using Application.Dtos.User;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthApplication _authApplication;

    public AuthController(IAuthApplication authApplication)
    {
        _authApplication = authApplication;
    }

    [AllowAnonymous]
    [HttpPost("IniciarSesion")]
    public async Task<IActionResult> IniciarSesion([FromBody] LoginRequestDto request)
    {
        var response = await _authApplication.LoginAsync(request);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("RegistrarUsuario")]
    public async Task<IActionResult> RegistrarUsuario([FromBody] RegisterRequestDto request)
    {
        var response = await _authApplication.RegisterAsync(request);
        return Ok(response);
    }

    [HttpGet("ListarUsuarios")]
    public async Task<IActionResult> ListarUsuarios()
    {
        var response = await _authApplication.GetAllUsersAsync();
        return Ok(response);
    }

    [HttpDelete("EliminarUsuario/{id}")]
    public async Task<IActionResult> EliminarUsuario(int id)
    {
        var response = await _authApplication.DeleteUserAsync(id);
        return Ok(response);
    }
}
