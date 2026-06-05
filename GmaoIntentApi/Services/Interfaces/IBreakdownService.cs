using GmaoIntentApi.DTOs;

namespace GmaoIntentApi.Services.Interfaces
{
    public interface IBreakdownService
    {
        Task<IReadOnlyCollection<BreakdownDto>> GetBreakdownsAsync();
        Task<BreakdownDto?> GetBreakdownByIdAsync(int id);
        Task<BreakdownDto?> CreateBreakdownAsync(CreateBreakdownDto dto);
        Task<bool?> UpdateBreakdownAsync(int id, UpdateBreakdownDto dto);
        Task<bool> DeleteBreakdownAsync(int id);
    }
}
