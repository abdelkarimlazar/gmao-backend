using GmaoIntentApi.Models;
using GmaoTaskStatus = GmaoIntentApi.Enums.TaskStatus;

namespace GmaoIntentApi.Repositories.Interfaces
{
    public interface IMaintenanceTaskRepository
    {
        Task<(IReadOnlyCollection<MaintenanceTask> Items, int TotalCount)> GetPagedAsync(GmaoTaskStatus? status, DateTime? startDateFrom, DateTime? startDateTo, int pageNumber, int pageSize);
        Task<IReadOnlyCollection<MaintenanceTask>> GetAllAsync();
        Task<MaintenanceTask?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<MaintenanceTask> AddAsync(MaintenanceTask task);
        Task UpdateAsync(MaintenanceTask task);
        Task DeleteAsync(MaintenanceTask task);
    }
}
