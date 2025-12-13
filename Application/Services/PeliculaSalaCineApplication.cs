using Application.Dtos.Commons.Response;
using Application.Dtos.PeliculaSalaCine;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Modelo.Entities;
using Repository.Context;
using Repository.Persistencias;

namespace Application.Services;

public class PeliculaSalaCineApplication : IPeliculaSalaCineApplication
{
    private readonly CineDbContext _context;

    public PeliculaSalaCineApplication(CineDbContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse<IEnumerable<PeliculaSalaCineResponseDto>>> GetAllAsync()
    {
        var response = new BaseResponse<IEnumerable<PeliculaSalaCineResponseDto>>();
        try
        {
            var peliculasSalas = await _context.PeliculaSalacines
                .Include(ps => ps.IdPeliculaNavigation)
                .Include(ps => ps.IdSalaCineNavigation)
                .Where(ps => !ps.Eliminado)
                .Select(ps => new PeliculaSalaCineResponseDto
                {
                    IdPeliculaSala = ps.IdPeliculaSala,
                    IdSalaCine = ps.IdSalaCine,
                    IdPelicula = ps.IdPelicula,
                    NombrePelicula = ps.IdPeliculaNavigation.Nombre,
                    NombreSala = ps.IdSalaCineNavigation.Nombre,
                    FechaPublicacion = ps.FechaPublicacion,
                    FechaFin = ps.FechaFin,
                    Eliminado = ps.Eliminado,
                    CreatedAt = ps.CreatedAt,
                    UpdatedAt = ps.UpdatedAt
                })
                .ToListAsync();

            if (peliculasSalas.Any())
            {
                response.IsSuccess = true;
                response.Data = peliculasSalas;
                response.TotalRecords = peliculasSalas.Count;
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

    public async Task<BaseResponse<PeliculaSalaCineResponseDto>> GetByIdAsync(int id)
    {
        var response = new BaseResponse<PeliculaSalaCineResponseDto>();
        try
        {
            var peliculaSala = await _context.PeliculaSalacines
                .Include(ps => ps.IdPeliculaNavigation)
                .Include(ps => ps.IdSalaCineNavigation)
                .Where(ps => ps.IdPeliculaSala == id && !ps.Eliminado)
                .Select(ps => new PeliculaSalaCineResponseDto
                {
                    IdPeliculaSala = ps.IdPeliculaSala,
                    IdSalaCine = ps.IdSalaCine,
                    IdPelicula = ps.IdPelicula,
                    NombrePelicula = ps.IdPeliculaNavigation.Nombre,
                    NombreSala = ps.IdSalaCineNavigation.Nombre,
                    FechaPublicacion = ps.FechaPublicacion,
                    FechaFin = ps.FechaFin,
                    Eliminado = ps.Eliminado,
                    CreatedAt = ps.CreatedAt,
                    UpdatedAt = ps.UpdatedAt
                })
                .FirstOrDefaultAsync();

            if (peliculaSala is not null)
            {
                response.IsSuccess = true;
                response.Data = peliculaSala;
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

    public async Task<BaseResponse<IEnumerable<PeliculaSalaCineResponseDto>>> GetByPeliculaIdAsync(int peliculaId)
    {
        var response = new BaseResponse<IEnumerable<PeliculaSalaCineResponseDto>>();
        try
        {
            var peliculasSalas = await _context.PeliculaSalacines
                .Include(ps => ps.IdPeliculaNavigation)
                .Include(ps => ps.IdSalaCineNavigation)
                .Where(ps => ps.IdPelicula == peliculaId && !ps.Eliminado)
                .Select(ps => new PeliculaSalaCineResponseDto
                {
                    IdPeliculaSala = ps.IdPeliculaSala,
                    IdSalaCine = ps.IdSalaCine,
                    IdPelicula = ps.IdPelicula,
                    NombrePelicula = ps.IdPeliculaNavigation.Nombre,
                    NombreSala = ps.IdSalaCineNavigation.Nombre,
                    FechaPublicacion = ps.FechaPublicacion,
                    FechaFin = ps.FechaFin,
                    Eliminado = ps.Eliminado,
                    CreatedAt = ps.CreatedAt,
                    UpdatedAt = ps.UpdatedAt
                })
                .ToListAsync();

            if (peliculasSalas.Any())
            {
                response.IsSuccess = true;
                response.Data = peliculasSalas;
                response.TotalRecords = peliculasSalas.Count;
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

    public async Task<BaseResponse<IEnumerable<PeliculaSalaCineResponseDto>>> GetBySalaCineIdAsync(int salaCineId)
    {
        var response = new BaseResponse<IEnumerable<PeliculaSalaCineResponseDto>>();
        try
        {
            var peliculasSalas = await _context.PeliculaSalacines
                .Include(ps => ps.IdPeliculaNavigation)
                .Include(ps => ps.IdSalaCineNavigation)
                .Where(ps => ps.IdSalaCine == salaCineId && !ps.Eliminado)
                .Select(ps => new PeliculaSalaCineResponseDto
                {
                    IdPeliculaSala = ps.IdPeliculaSala,
                    IdSalaCine = ps.IdSalaCine,
                    IdPelicula = ps.IdPelicula,
                    NombrePelicula = ps.IdPeliculaNavigation.Nombre,
                    NombreSala = ps.IdSalaCineNavigation.Nombre,
                    FechaPublicacion = ps.FechaPublicacion,
                    FechaFin = ps.FechaFin,
                    Eliminado = ps.Eliminado,
                    CreatedAt = ps.CreatedAt,
                    UpdatedAt = ps.UpdatedAt
                })
                .ToListAsync();

            if (peliculasSalas.Any())
            {
                response.IsSuccess = true;
                response.Data = peliculasSalas;
                response.TotalRecords = peliculasSalas.Count;
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

    public async Task<BaseResponse<bool>> CreateAsync(PeliculaSalaCineRequestDto request)
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
                Nombre = request.NombrePelicula,
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

    public async Task<BaseResponse<bool>> UpdateAsync(int id, PeliculaSalaCineRequestDto request)
    {
        var response = new BaseResponse<bool>();
        try
        {
            // Obtener la relación película-sala con la película asociada
            var peliculaSala = await _context.PeliculaSalacines
                .Include(ps => ps.IdPeliculaNavigation)
                .FirstOrDefaultAsync(ps => ps.IdPeliculaSala == id);

            if (peliculaSala is null || peliculaSala.Eliminado)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            // Verificar que la nueva sala exista
            var salaExists = await _context.SalaCines
                .AnyAsync(s => s.IdSala == request.IdSalaCine && !s.Eliminado);

            if (!salaExists)
            {
                response.IsSuccess = false;
                response.Message = "La sala de cine no existe o está eliminada.";
                return response;
            }

            // Actualizar los datos de la película
            var pelicula = peliculaSala.IdPeliculaNavigation;
            pelicula.Nombre = request.NombrePelicula;
            pelicula.Duracion = request.Duracion;
            pelicula.UpdatedAt = DateTime.UtcNow;

            // Actualizar la relación película-sala
            peliculaSala.IdSalaCine = request.IdSalaCine;
            peliculaSala.FechaPublicacion = request.FechaPublicacion;
            peliculaSala.FechaFin = request.FechaFin;
            peliculaSala.UpdatedAt = DateTime.UtcNow;

            _context.Peliculas.Update(pelicula);
            _context.PeliculaSalacines.Update(peliculaSala);
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
            var peliculaSala = await _context.PeliculaSalacines.FindAsync(id);

            if (peliculaSala is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            // Soft Delete
            peliculaSala.Eliminado = true;
            peliculaSala.UpdatedAt = DateTime.UtcNow;

            _context.PeliculaSalacines.Update(peliculaSala);
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
            var peliculaSala = await _context.PeliculaSalacines.FindAsync(id);

            if (peliculaSala is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            // Reactivar
            peliculaSala.Eliminado = false;
            peliculaSala.UpdatedAt = DateTime.UtcNow;

            _context.PeliculaSalacines.Update(peliculaSala);
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

    public async Task<BaseResponse<IEnumerable<PeliculaSalaCineResponseDto>>> GetByFechaPublicacionAsync(DateOnly fecha)
    {
        var response = new BaseResponse<IEnumerable<PeliculaSalaCineResponseDto>>();
        try
        {
            var peliculasSalas = await _context.PeliculaSalacines
                .Include(ps => ps.IdPeliculaNavigation)
                .Include(ps => ps.IdSalaCineNavigation)
                .Where(ps => ps.FechaPublicacion == fecha && !ps.Eliminado)
                .Select(ps => new PeliculaSalaCineResponseDto
                {
                    IdPeliculaSala = ps.IdPeliculaSala,
                    IdSalaCine = ps.IdSalaCine,
                    IdPelicula = ps.IdPelicula,
                    NombrePelicula = ps.IdPeliculaNavigation.Nombre,
                    NombreSala = ps.IdSalaCineNavigation.Nombre,
                    FechaPublicacion = ps.FechaPublicacion,
                    FechaFin = ps.FechaFin,
                    Eliminado = ps.Eliminado,
                    CreatedAt = ps.CreatedAt,
                    UpdatedAt = ps.UpdatedAt
                })
                .ToListAsync();

            if (peliculasSalas.Any())
            {
                response.IsSuccess = true;
                response.Data = peliculasSalas;
                response.TotalRecords = peliculasSalas.Count;
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
