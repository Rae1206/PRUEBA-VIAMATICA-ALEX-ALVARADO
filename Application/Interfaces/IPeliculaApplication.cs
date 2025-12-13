using Application.Dtos.Commons.Response;
using Application.Dtos.Pelicula;

namespace Application.Interfaces;

public interface IPeliculaApplication
{
    Task<BaseResponse<IEnumerable<PeliculaResponseDto>>> GetAllAsync();
    Task<BaseResponse<PeliculaResponseDto>> GetByIdAsync(int id);
    Task<BaseResponse<bool>> CreateAsync(PeliculaRequestDto request);
    Task<BaseResponse<bool>> UpdateAsync(int id, PeliculaRequestDto request);
    Task<BaseResponse<bool>> DeleteAsync(int id);  // Soft Delete
    Task<BaseResponse<bool>> ActivateAsync(int id);
    Task<BaseResponse<IEnumerable<PeliculaResponseDto>>> GetByNombreAsync(string nombre);
}
