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

    [HttpGet("ObtenerSalaCine/{id}")]
    public async Task<IActionResult> ObtenerSalaCine(int id)
    {
        var response = await _salaCineApplication.GetByIdAsync(id);
        return Ok(response);
    }

    [HttpPost("CrearSalaCine")]
    public async Task<IActionResult> CrearSalaCine([FromBody] SalaCineRequestDto request)
    {
        var response = await _salaCineApplication.CreateAsync(request);
        return Ok(response);
    }

    [HttpPut("ActualizarSalaCine/{id}")]
    public async Task<IActionResult> ActualizarSalaCine(int id, [FromBody] SalaCineRequestDto request)
    {
        var response = await _salaCineApplication.UpdateAsync(id, request);
        return Ok(response);
    }

    [HttpDelete("EliminarSalaCine/{id}")]
    public async Task<IActionResult> EliminarSalaCine(int id)
    {
        var response = await _salaCineApplication.DeleteAsync(id);
        return Ok(response);
    }

    [HttpPut("ActivarSalaCine/{id}")]
    public async Task<IActionResult> ActivarSalaCine(int id)
    {
        var response = await _salaCineApplication.ActivateAsync(id);
        return Ok(response);
    }

    [HttpGet("DisponibilidadPorNombre/{nombre}")]
    public async Task<IActionResult> GetDisponibilidadByNombre(string nombre)
    {
        var response = await _salaCineApplication.GetDisponibilidadByNombreAsync(nombre);
        return Ok(response);
    }
}
