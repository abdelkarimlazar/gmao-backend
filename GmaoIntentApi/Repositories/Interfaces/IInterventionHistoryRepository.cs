using GmaoIntentApi.Models;

namespace GmaoIntentApi.Repositories.Interfaces
{
    public interface IInterventionHistoryRepository
    {
        Task<IReadOnlyCollection<InterventionHistory>> GetAllAsync();
        Task<IReadOnlyCollection<InterventionHistory>> GetByTaskIdAsync(int taskId);
        Task<InterventionHistory?> GetByIdAsync(int id);
        Task<InterventionHistory> AddAsync(InterventionHistory history);
        Task UpdateAsync(InterventionHistory history);
        Task DeleteAsync(InterventionHistory history);
    }
}
