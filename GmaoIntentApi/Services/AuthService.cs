using GmaoIntentApi.DTOs;
using GmaoIntentApi.Models;
using GmaoIntentApi.Repositories.Interfaces;
using GmaoIntentApi.Services.Interfaces;

namespace GmaoIntentApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, IPasswordHasherService passwordHasherService, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordHasherService = passwordHasherService;
            _tokenService = tokenService;
        }

        public async Task<(bool Succeeded, string? Error, AuthResponseDto? Response)> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser is not null)
            {
                return (false, "A user with this email already exists.", null);
            }

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = _passwordHasherService.HashPassword(dto.Password),
                Role = dto.Role
            };

            await _userRepository.AddAsync(user);
            var token = _tokenService.CreateToken(user);

            return (true, null, new AuthResponseDto
            {
                Token = token.Token,
                ExpiresAt = token.ExpiresAt,
                User = new UserDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role
                }
            });
        }

        public async Task<(bool Succeeded, string? Error, AuthResponseDto? Response)> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user is null || !_passwordHasherService.VerifyPassword(dto.Password, user.PasswordHash))
            {
                return (false, "Invalid email or password.", null);
            }

            var token = _tokenService.CreateToken(user);

            return (true, null, new AuthResponseDto
            {
                Token = token.Token,
                ExpiresAt = token.ExpiresAt,
                User = new UserDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role
                }
            });
        }
    }
}
