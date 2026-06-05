using GmaoIntentApi.DTOs;

namespace GmaoIntentApi.Services.Interfaces
{
    public interface IInterventionHistoryService
    {
        Task<IReadOnlyCollection<InterventionHistoryDto>> GetInterventionHistoriesAsync();
        Task<IReadOnlyCollection<InterventionHistoryDto>> GetInterventionHistoriesByTaskAsync(int taskId);
        Task<InterventionHistoryDto?> GetInterventionHistoryByIdAsync(int id);
        Task<InterventionHistoryDto?> CreateInterventionHistoryAsync(CreateInterventionHistoryDto dto);
        Task<bool?> UpdateInterventionHistoryAsync(int id, UpdateInterventionHistoryDto dto);
        Task<bool> DeleteInterventionHistoryAsync(int id);
    }
}
