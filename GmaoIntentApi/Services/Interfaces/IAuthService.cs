using GmaoIntentApi.DTOs;

namespace GmaoIntentApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Succeeded, string? Error, AuthResponseDto? Response)> RegisterAsync(RegisterDto dto);
        Task<(bool Succeeded, string? Error, AuthResponseDto? Response)> LoginAsync(LoginDto dto);
    }
}
