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

    [HttpGet("ObtenerPeliculaSalaCine/{id}")]
    public async Task<IActionResult> ObtenerPeliculaSalaCine(int id)
    {
        var response = await _peliculaSalaCineApplication.GetByIdAsync(id);
        return Ok(response);
    }

    [HttpGet("BuscarPorPelicula/{peliculaId}")]
    public async Task<IActionResult> BuscarPorPelicula(int peliculaId)
    {
        var response = await _peliculaSalaCineApplication.GetByPeliculaIdAsync(peliculaId);
        return Ok(response);
    }

    [HttpGet("BuscarPorSalaCine/{salaCineId}")]
    public async Task<IActionResult> BuscarPorSalaCine(int salaCineId)
    {
        var response = await _peliculaSalaCineApplication.GetBySalaCineIdAsync(salaCineId);
        return Ok(response);
    }

    [HttpPost("CrearPeliculaSalaCine")]
    public async Task<IActionResult> CrearPeliculaSalaCine([FromBody] PeliculaSalaCineRequestDto request)
    {
        var response = await _peliculaSalaCineApplication.CreateAsync(request);
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

    [HttpPut("ActivarPeliculaSalaCine/{id}")]
    public async Task<IActionResult> ActivarPeliculaSalaCine(int id)
    {
        var response = await _peliculaSalaCineApplication.ActivateAsync(id);
        return Ok(response);
    }

    [HttpGet("BuscarPorFechaPublicacion/{fecha}")]
    public async Task<IActionResult> BuscarPorFechaPublicacion(DateOnly fecha)
    {
        var response = await _peliculaSalaCineApplication.GetByFechaPublicacionAsync(fecha);
        return Ok(response);
    }
}
