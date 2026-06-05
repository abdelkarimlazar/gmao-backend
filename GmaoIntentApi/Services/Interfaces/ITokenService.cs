using GmaoIntentApi.Models;

namespace GmaoIntentApi.Services.Interfaces
{
    public interface ITokenService
    {
        (string Token, DateTime ExpiresAt) CreateToken(User user);
    }
}
