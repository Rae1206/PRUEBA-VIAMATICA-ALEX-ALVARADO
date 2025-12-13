using Application.Dtos.Commons.Response;
using Application.Dtos.User;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Modelo.Entities;
using Repository.Context;
using Repository.Persistencias;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace Application.Services;

public class AuthApplication : IAuthApplication
{
    private readonly CineDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthApplication(CineDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<BaseResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request)
    {
        var response = new BaseResponse<LoginResponseDto>();
        try
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email && !u.Eliminado);

            if (user is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_TOKEN_ERROR;
                return response;
            }

            bool isPasswordValid = false;
            try
            {
                isPasswordValid = BC.Verify(request.Password, user.Password);
            }
            catch (Exception)
            {
                isPasswordValid = false;
            }

            // Lazy migration for plain text passwords
            if (!isPasswordValid)
            {
                if (request.Password == user.Password)
                {
                    user.Password = BC.HashPassword(request.Password);
                    user.UpdatedAt = DateTime.UtcNow;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                    isPasswordValid = true;
                }
            }

            if (!isPasswordValid)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_TOKEN_ERROR;
                return response;
            }

            var token = GenerateToken(user);

            response.IsSuccess = true;
            response.Data = new LoginResponseDto
            {
                Token = token,
                UserName = user.UserName,
                Email = user.Email
            };
            response.Message = ReplyMessage.MESSAGE_TOKEN;
        }
        catch (Exception)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_EXCEPTION;
        }
        return response;
    }

    public async Task<BaseResponse<UserResponseDto>> RegisterAsync(RegisterRequestDto request)
    {
        var response = new BaseResponse<UserResponseDto>();
        try
        {
            var emailExists = await _context.Users
                .AnyAsync(u => u.Email == request.Email && !u.Eliminado);

            if (emailExists)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXISTS;
                return response;
            }

            var hashedPassword = BC.HashPassword(request.Password);

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                Password = hashedPassword,
                Eliminado = false,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            response.IsSuccess = true;
            response.Data = new UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };
            response.Message = ReplyMessage.MESSAGE_SAVE;
        }
        catch (Exception)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_EXCEPTION;
        }
        return response;
    }

    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var now = DateTime.UtcNow;
        var expiration = int.Parse(_configuration["Jwt:Expires"]!);
        var expiresAt = now.AddHours(expiration);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.Email!),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.UserName!),
            new Claim(JwtRegisteredClaimNames.GivenName, user.Email!),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Issuer"],
            claims: claims,
            expires: expiresAt,
            notBefore: now,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
