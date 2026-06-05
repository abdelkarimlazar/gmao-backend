using GmaoIntentApi.Models;

namespace GmaoIntentApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<(IReadOnlyCollection<User> Items, int TotalCount)> GetPagedAsync(string? search, int pageNumber, int pageSize);
        Task<IReadOnlyCollection<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<User> AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
