using Application.Dtos.Commons.Response;
using Application.Dtos.PeliculaSalaCine;

namespace Application.Interfaces;

public interface IPeliculaSalaCineApplication
{
    Task<BaseResponse<IEnumerable<PeliculaSalaCineResponseDto>>> GetAllAsync();
    Task<BaseResponse<bool>> UpdateAsync(int id, PeliculaSalaCineRequestDto request);
    Task<BaseResponse<bool>> DeleteAsync(int id);
}
