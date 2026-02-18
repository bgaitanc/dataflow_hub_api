using DataFlowHub.Application.Models.Auth;

namespace DataFlowHub.Application.Services.Interfaces;

public interface IAuthService
{
    Task<Guid> RegisterUserAsync(RegisterDto registerDto);

    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    
    Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
}