using GmaoIntentApi.Enums;
using GmaoIntentApi.Models;

namespace GmaoIntentApi.Repositories.Interfaces
{
    public interface IEquipmentRepository
    {
        Task<(IReadOnlyCollection<Equipment> Items, int TotalCount)> GetPagedAsync(EquipmentStatus? status, int pageNumber, int pageSize);
        Task<IReadOnlyCollection<Equipment>> GetAllAsync();
        Task<Equipment?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<Equipment> AddAsync(Equipment equipment);
        Task UpdateAsync(Equipment equipment);
        Task DeleteAsync(Equipment equipment);
    }
}
