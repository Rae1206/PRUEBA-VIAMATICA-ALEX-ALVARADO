using Application.Dtos.Commons.Response;
using Application.Dtos.SalaCine;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Modelo.Entities;
using Repository.Context;
using Repository.Persistencias;

namespace Application.Services;

public class SalaCineApplication : ISalaCineApplication
{
    private readonly CineDbContext _context;

    public SalaCineApplication(CineDbContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse<IEnumerable<SalaCineResponseDto>>> GetAllAsync()
    {
        var response = new BaseResponse<IEnumerable<SalaCineResponseDto>>();
        try
        {
            var salas = await _context.SalaCines
                .Where(s => !s.Eliminado)
                .Select(s => new SalaCineResponseDto
                {
                    IdSala = s.IdSala,
                    Nombre = s.Nombre,
                    Eliminado = s.Eliminado,
                    CreatedAt = s.CreatedAt,
                    UpdatedAt = s.UpdatedAt
                })
                .ToListAsync();

            if (salas.Any())
            {
                response.IsSuccess = true;
                response.Data = salas;
                response.TotalRecords = salas.Count;
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

    public async Task<BaseResponse<bool>> CreateAsync(SalaCineRequestDto request)
    {
        var response = new BaseResponse<bool>();
        try
        {
            var exists = await _context.SalaCines
                .AnyAsync(s => s.Nombre == request.Nombre && !s.Eliminado);

            if (exists)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXISTS;
                return response;
            }

            var sala = new SalaCine
            {
                Nombre = request.Nombre,
                Eliminado = false,
                CreatedAt = DateTime.UtcNow
            };

            await _context.SalaCines.AddAsync(sala);
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

    public async Task<BaseResponse<SalaDisponibilidadDto>> GetDisponibilidadByNombreAsync(string nombreSala)
    {
        var response = new BaseResponse<SalaDisponibilidadDto>();
        try
        {
            var result = await _context.Database
                .SqlQuery<SalaDisponibilidadDto>($"EXEC usp_disponibilidad_sala_cine_por_nombre {nombreSala}")
                .ToListAsync();

            if (result.Any())
            {
                response.IsSuccess = true;
                response.Data = result.First();
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "No se encontró información para la sala especificada.";
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
