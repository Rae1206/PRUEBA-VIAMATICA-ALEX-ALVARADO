using Application.Dtos.Commons.Response;
using Application.Dtos.Pelicula;

namespace Application.Interfaces;

public interface IPeliculaApplication
{
    Task<BaseResponse<bool>> CreateAsync(PeliculaRequestDto request);
}
