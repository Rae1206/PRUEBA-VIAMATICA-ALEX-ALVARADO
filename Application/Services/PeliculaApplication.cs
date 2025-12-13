using Application.Dtos.Commons.Response;
using Application.Dtos.Pelicula;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Modelo.Entities;
using Repository.Context;
using Repository.Persistencias;

namespace Application.Services;

public class PeliculaApplication : IPeliculaApplication
{
    private readonly CineDbContext _context;

    public PeliculaApplication(CineDbContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse<bool>> CreateAsync(PeliculaRequestDto request)
    {
        var response = new BaseResponse<bool>();
        try
        {
            var salaExists = await _context.SalaCines
                .AnyAsync(s => s.IdSala == request.IdSalaCine && !s.Eliminado);

            if (!salaExists)
            {
                response.IsSuccess = false;
                response.Message = "La sala de cine no existe o está eliminada.";
                return response;
            }

            var pelicula = new Pelicula
            {
                Nombre = request.Nombre,
                Duracion = request.Duracion,
                Eliminado = false,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Peliculas.AddAsync(pelicula);
            await _context.SaveChangesAsync();

            var peliculaSala = new PeliculaSalacine
            {
                IdPelicula = pelicula.IdPelicula,
                IdSalaCine = request.IdSalaCine,
                FechaPublicacion = request.FechaPublicacion,
                FechaFin = request.FechaFin,
                Eliminado = false,
                CreatedAt = DateTime.UtcNow
            };

            await _context.PeliculaSalacines.AddAsync(peliculaSala);
            await _context.SaveChangesAsync();

            response.IsSuccess = true;
            response.Data = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;
        }
        catch (Exception)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_EXCEPTION;
        }
        return response;
    }
}
