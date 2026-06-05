using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using GmaoIntentApi.Models;
using GmaoIntentApi.Options;
using GmaoIntentApi.Services.Interfaces;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GmaoIntentApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _jwtOptions;

        public TokenService(
            IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public (string Token, DateTime ExpiresAt)
            CreateToken(User user)
        {
            var key =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        _jwtOptions.Key));

            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            var expiresAt =
                DateTime.UtcNow.AddMinutes(
                    _jwtOptions.ExpirationMinutes);

            var claims = new List<Claim>
            {
                new(
                    JwtRegisteredClaimNames.Sub,
                    user.Id.ToString()),

                new(
                    JwtRegisteredClaimNames.Email,
                    user.Email),

                new(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()),

                new(
                    ClaimTypes.Name,
                    user.FullName),

                new(
                    ClaimTypes.Email,
                    user.Email),

                new Claim(
                    ClaimTypes.Role,
                    user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,

                audience: _jwtOptions.Audience,

                claims: claims,

                expires: expiresAt,

                signingCredentials: credentials);

            return
            (
                new JwtSecurityTokenHandler()
                    .WriteToken(token),

                expiresAt
            );
        }
    }
}