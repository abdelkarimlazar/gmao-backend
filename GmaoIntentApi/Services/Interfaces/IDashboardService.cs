using GmaoIntentApi.DTOs;

namespace GmaoIntentApi.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetSummaryAsync();
    }
}
