using Application.Dtos.Commons.Response;
using Application.Dtos.SalaCine;

namespace Application.Interfaces;

public interface ISalaCineApplication
{
    Task<BaseResponse<IEnumerable<SalaCineResponseDto>>> GetAllAsync();
    Task<BaseResponse<bool>> CreateAsync(SalaCineRequestDto request);
    Task<BaseResponse<SalaDisponibilidadDto>> GetDisponibilidadByNombreAsync(string nombreSala);
}
