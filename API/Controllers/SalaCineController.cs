using Application.Dtos.SalaCine;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalaCineController : ControllerBase
{
    private readonly ISalaCineApplication _salaCineApplication;

    public SalaCineController(ISalaCineApplication salaCineApplication)
    {
        _salaCineApplication = salaCineApplication;
    }

    [HttpGet("ListarSalasCine")]
    public async Task<IActionResult> ListarSalasCine()
    {
        var response = await _salaCineApplication.GetAllAsync();
        return Ok(response);
    }

    [HttpPost("CrearSalaCine")]
    public async Task<IActionResult> CrearSalaCine([FromBody] SalaCineRequestDto request)
    {
        var response = await _salaCineApplication.CreateAsync(request);
        return Ok(response);
    }

    [HttpGet("DisponibilidadPorNombre/{nombre}")]
    public async Task<IActionResult> GetDisponibilidadByNombre(string nombre)
    {
        var response = await _salaCineApplication.GetDisponibilidadByNombreAsync(nombre);
        return Ok(response);
    }
}
