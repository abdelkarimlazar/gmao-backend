using GmaoIntentApi.DTOs;

namespace GmaoIntentApi.Services.Interfaces
{
    public interface IMaintenanceTaskService
    {
        Task<PagedResultDto<MaintenanceTaskDto>> GetMaintenanceTasksAsync(MaintenanceTaskQueryDto query);
        Task<MaintenanceTaskDto?> GetMaintenanceTaskByIdAsync(int id);
        Task<MaintenanceTaskDto?> CreateMaintenanceTaskAsync(CreateMaintenanceTaskDto dto);
        Task<bool?> UpdateMaintenanceTaskAsync(int id, UpdateMaintenanceTaskDto dto);
        Task<bool> DeleteMaintenanceTaskAsync(int id);
    }
}
