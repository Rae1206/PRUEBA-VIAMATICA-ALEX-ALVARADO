using Application.Dtos.Commons.Response;
using Application.Dtos.SalaCine;

namespace Application.Interfaces;

public interface ISalaCineApplication
{
    Task<BaseResponse<IEnumerable<SalaCineResponseDto>>> GetAllAsync();
    Task<BaseResponse<SalaCineResponseDto>> GetByIdAsync(int id);
    Task<BaseResponse<bool>> CreateAsync(SalaCineRequestDto request);
    Task<BaseResponse<bool>> UpdateAsync(int id, SalaCineRequestDto request);
    Task<BaseResponse<bool>> DeleteAsync(int id); 
    Task<BaseResponse<SalaDisponibilidadDto>> GetDisponibilidadByNombreAsync(string nombreSala);

}
