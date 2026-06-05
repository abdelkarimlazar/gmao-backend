using GmaoIntentApi.Models;
using GmaoIntentApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GmaoIntentApi.Services
{
    public class PasswordHasherService : IPasswordHasherService
    {
        private readonly PasswordHasher<User> _passwordHasher = new();

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(new User(), password);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            var result = _passwordHasher.VerifyHashedPassword(new User(), passwordHash, password);
            return result is PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}
