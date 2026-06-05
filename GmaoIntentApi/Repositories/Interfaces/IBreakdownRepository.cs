using GmaoIntentApi.Models;

namespace GmaoIntentApi.Repositories.Interfaces
{
    public interface IBreakdownRepository
    {
        Task<IReadOnlyCollection<Breakdown>> GetAllAsync();
        Task<Breakdown?> GetByIdAsync(int id);
        Task<Breakdown> AddAsync(Breakdown breakdown);
        Task UpdateAsync(Breakdown breakdown);
        Task DeleteAsync(Breakdown breakdown);
    }
}
