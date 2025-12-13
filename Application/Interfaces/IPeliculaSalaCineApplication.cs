using Application.Dtos.Commons.Response;
using Application.Dtos.PeliculaSalaCine;

namespace Application.Interfaces;

public interface IPeliculaSalaCineApplication
{
    Task<BaseResponse<IEnumerable<PeliculaSalaCineResponseDto>>> GetAllAsync();
    Task<BaseResponse<PeliculaSalaCineResponseDto>> GetByIdAsync(int id);
    Task<BaseResponse<IEnumerable<PeliculaSalaCineResponseDto>>> GetByPeliculaIdAsync(int peliculaId);
    Task<BaseResponse<IEnumerable<PeliculaSalaCineResponseDto>>> GetBySalaCineIdAsync(int salaCineId);
    Task<BaseResponse<IEnumerable<PeliculaSalaCineResponseDto>>> GetByFechaPublicacionAsync(DateOnly fecha);
    Task<BaseResponse<bool>> CreateAsync(PeliculaSalaCineRequestDto request);
    Task<BaseResponse<bool>> UpdateAsync(int id, PeliculaSalaCineRequestDto request);
    Task<BaseResponse<bool>> DeleteAsync(int id);  // Soft Delete
    Task<BaseResponse<bool>> ActivateAsync(int id);  // Reactivar
}
