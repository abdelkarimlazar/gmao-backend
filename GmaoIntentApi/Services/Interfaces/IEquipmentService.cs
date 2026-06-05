using GmaoIntentApi.DTOs;
using GmaoIntentApi.Enums;

namespace GmaoIntentApi.Services.Interfaces
{
    public interface IEquipmentService
    {
        Task<(IReadOnlyCollection<EquipmentDto> Items, int TotalCount)>
            GetPagedAsync(
                EquipmentStatus? status,
                int pageNumber,
                int pageSize);

        Task<IReadOnlyCollection<EquipmentDto>> GetAllAsync();

        Task<EquipmentDto?> GetByIdAsync(int id);

        Task<EquipmentDto> CreateAsync(CreateEquipmentDto dto);

        Task UpdateAsync(int id, UpdateEquipmentDto dto);

        Task DeleteAsync(int id);
    }
}