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

    public async Task<BaseResponse<IEnumerable<PeliculaResponseDto>>> GetAllAsync()
    {
        var response = new BaseResponse<IEnumerable<PeliculaResponseDto>>();
        try
        {
            var peliculas = await _context.Peliculas
                .Where(p => !p.Eliminado)
                .Select(p => new PeliculaResponseDto
                {
                    IdPelicula = p.IdPelicula,
                    Nombre = p.Nombre,
                    Duracion = p.Duracion,
                    Eliminado = p.Eliminado,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .ToListAsync();

            if (peliculas.Any())
            {
                response.IsSuccess = true;
                response.Data = peliculas;
                response.TotalRecords = peliculas.Count;
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
        }
        catch (Exception)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_EXCEPTION;
        }
        return response;
    }

    public async Task<BaseResponse<PeliculaResponseDto>> GetByIdAsync(int id)
    {
        var response = new BaseResponse<PeliculaResponseDto>();
        try
        {
            var pelicula = await _context.Peliculas
                .Where(p => p.IdPelicula == id && !p.Eliminado)
                .Select(p => new PeliculaResponseDto
                {
                    IdPelicula = p.IdPelicula,
                    Nombre = p.Nombre,
                    Duracion = p.Duracion,
                    Eliminado = p.Eliminado,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .FirstOrDefaultAsync();

            if (pelicula is not null)
            {
                response.IsSuccess = true;
                response.Data = pelicula;
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
        }
        catch (Exception)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_EXCEPTION;
        }
        return response;
    }

    public async Task<BaseResponse<bool>> CreateAsync(PeliculaRequestDto request)
    {
        var response = new BaseResponse<bool>();
        try
        {
            // Verificar que la sala exista
            var salaExists = await _context.SalaCines
                .AnyAsync(s => s.IdSala == request.IdSalaCine && !s.Eliminado);

            if (!salaExists)
            {
                response.IsSuccess = false;
                response.Message = "La sala de cine no existe o está eliminada.";
                return response;
            }

            // Crear la película
            var pelicula = new Pelicula
            {
                Nombre = request.Nombre,
                Duracion = request.Duracion,
                Eliminado = false,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Peliculas.AddAsync(pelicula);
            await _context.SaveChangesAsync();

            // Crear la relación película-sala
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

    public async Task<BaseResponse<bool>> UpdateAsync(int id, PeliculaRequestDto request)
    {
        var response = new BaseResponse<bool>();
        try
        {
            var pelicula = await _context.Peliculas.FindAsync(id);

            if (pelicula is null || pelicula.Eliminado)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            pelicula.Nombre = request.Nombre;
            pelicula.Duracion = request.Duracion;
            pelicula.UpdatedAt = DateTime.UtcNow;

            _context.Peliculas.Update(pelicula);
            await _context.SaveChangesAsync();

            response.IsSuccess = true;
            response.Data = true;
            response.Message = ReplyMessage.MESSAGE_UPDATE;
        }
        catch (Exception)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_EXCEPTION;
        }
        return response;
    }

    public async Task<BaseResponse<bool>> DeleteAsync(int id)
    {
        var response = new BaseResponse<bool>();
        try
        {
            var pelicula = await _context.Peliculas.FindAsync(id);

            if (pelicula is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            // Soft Delete - solo marca como eliminado
            pelicula.Eliminado = true;
            pelicula.UpdatedAt = DateTime.UtcNow;

            _context.Peliculas.Update(pelicula);
            await _context.SaveChangesAsync();

            response.IsSuccess = true;
            response.Data = true;
            response.Message = ReplyMessage.MESSAGE_DELETE;
        }
        catch (Exception)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_EXCEPTION;
        }
        return response;
    }

    public async Task<BaseResponse<bool>> ActivateAsync(int id)
    {
        var response = new BaseResponse<bool>();
        try
        {
            var pelicula = await _context.Peliculas.FindAsync(id);

            if (pelicula is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            // Reactivar - marca como no eliminado
            pelicula.Eliminado = false;
            pelicula.UpdatedAt = DateTime.UtcNow;

            _context.Peliculas.Update(pelicula);
            await _context.SaveChangesAsync();

            response.IsSuccess = true;
            response.Data = true;
            response.Message = ReplyMessage.MESSAGE_ACTIVATE;
        }
        catch (Exception)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_EXCEPTION;
        }
        return response;
    }


    public async Task<BaseResponse<IEnumerable<PeliculaResponseDto>>> GetByNombreAsync(string nombre)
    {
        var response = new BaseResponse<IEnumerable<PeliculaResponseDto>>();
        try
        {
            var peliculas = await _context.Peliculas
                .Where(p => p.Nombre.Contains(nombre) && !p.Eliminado)
                .Select(p => new PeliculaResponseDto
                {
                    IdPelicula = p.IdPelicula,
                    Nombre = p.Nombre,
                    Duracion = p.Duracion,
                    Eliminado = p.Eliminado,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .ToListAsync();

            if (peliculas.Any())
            {
                response.IsSuccess = true;
                response.Data = peliculas;
                response.TotalRecords = peliculas.Count;
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
        }
        catch (Exception)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_EXCEPTION;
        }
        return response;
    }
}
