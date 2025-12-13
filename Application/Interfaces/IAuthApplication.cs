using Application.Dtos.Commons.Response;
using Application.Dtos.User;

namespace Application.Interfaces;

public interface IAuthApplication
{
    Task<BaseResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request);
    Task<BaseResponse<UserResponseDto>> RegisterAsync(RegisterRequestDto request);
    Task<BaseResponse<IEnumerable<UserResponseDto>>> GetAllUsersAsync();
    Task<BaseResponse<bool>> DeleteUserAsync(int id); 

}
