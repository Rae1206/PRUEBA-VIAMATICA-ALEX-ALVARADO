using Application.Dtos.PeliculaSalaCine;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeliculaSalaCineController : ControllerBase
{
    private readonly IPeliculaSalaCineApplication _peliculaSalaCineApplication;

    public PeliculaSalaCineController(IPeliculaSalaCineApplication peliculaSalaCineApplication)
    {
        _peliculaSalaCineApplication = peliculaSalaCineApplication;
    }

    [HttpGet("ListarPeliculasSalaCine")]
    public async Task<IActionResult> ListarPeliculasSalaCine()
    {
        var response = await _peliculaSalaCineApplication.GetAllAsync();
        return Ok(response);
    }

    [HttpPut("ActualizarPeliculaSalaCine/{id}")]
    public async Task<IActionResult> ActualizarPeliculaSalaCine(int id, [FromBody] PeliculaSalaCineRequestDto request)
    {
        var response = await _peliculaSalaCineApplication.UpdateAsync(id, request);
        return Ok(response);
    }

    [HttpDelete("EliminarPeliculaSalaCine/{id}")]
    public async Task<IActionResult> EliminarPeliculaSalaCine(int id)
    {
        var response = await _peliculaSalaCineApplication.DeleteAsync(id);
        return Ok(response);
    }
}
