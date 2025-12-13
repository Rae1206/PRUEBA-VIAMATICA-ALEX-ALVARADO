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

    [HttpPost("CrearPelicula")]
    public async Task<IActionResult> CrearPelicula([FromBody] PeliculaRequestDto request)
    {
        var response = await _peliculaApplication.CreateAsync(request);
        return Ok(response);
    }
}
