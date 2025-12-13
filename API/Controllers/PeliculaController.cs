using Application.Dtos.Pelicula;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeliculaController : ControllerBase
{
    private readonly IPeliculaApplication _peliculaApplication;

    public PeliculaController(IPeliculaApplication peliculaApplication)
    {
        _peliculaApplication = peliculaApplication;
    }

    [HttpGet("ListarPeliculas")]
    public async Task<IActionResult> ListarPeliculas()
    {
        var response = await _peliculaApplication.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("ObtenerPelicula/{id}")]
    public async Task<IActionResult> ObtenerPelicula(int id)
    {
        var response = await _peliculaApplication.GetByIdAsync(id);
        return Ok(response);
    }

    [HttpPost("CrearPelicula")]
    public async Task<IActionResult> CrearPelicula([FromBody] PeliculaRequestDto request)
    {
        var response = await _peliculaApplication.CreateAsync(request);
        return Ok(response);
    }

    [HttpPut("ActualizarPelicula/{id}")]
    public async Task<IActionResult> ActualizarPelicula(int id, [FromBody] PeliculaRequestDto request)
    {
        var response = await _peliculaApplication.UpdateAsync(id, request);
        return Ok(response);
    }

    [HttpDelete("EliminarPelicula/{id}")]
    public async Task<IActionResult> EliminarPelicula(int id)
    {
        var response = await _peliculaApplication.DeleteAsync(id);
        return Ok(response);
    }

    [HttpPut("ActivarPelicula/{id}")]
    public async Task<IActionResult> ActivarPelicula(int id)
    {
        var response = await _peliculaApplication.ActivateAsync(id);
        return Ok(response);
    }

    [HttpGet("BuscarPorNombre/{nombre}")]
    public async Task<IActionResult> BuscarPorNombre(string nombre)
    {
        var response = await _peliculaApplication.GetByNombreAsync(nombre);
        return Ok(response);
    }
}
