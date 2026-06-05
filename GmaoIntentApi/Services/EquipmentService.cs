using GmaoIntentApi.DTOs;
using GmaoIntentApi.Enums;
using GmaoIntentApi.Models;
using GmaoIntentApi.Repositories.Interfaces;
using GmaoIntentApi.Services.Interfaces;

namespace GmaoIntentApi.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly INotificationService _notificationService;

        public EquipmentService(
            IEquipmentRepository equipmentRepository,
            INotificationService notificationService)
        {
            _equipmentRepository = equipmentRepository;
            _notificationService = notificationService;
        }

        public async Task<(IReadOnlyCollection<EquipmentDto> Items, int TotalCount)> GetPagedAsync(
            EquipmentStatus? status,
            int pageNumber,
            int pageSize)
        {
            var result = await _equipmentRepository.GetPagedAsync(
                status,
                pageNumber,
                pageSize);

            var items = result.Items
                .Select(e => new EquipmentDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    SerialNumber = e.SerialNumber,
                    Location = e.Location,
                    Status = e.Status
                })
                .ToList();

            return (items, result.TotalCount);
        }

        public async Task<IReadOnlyCollection<EquipmentDto>> GetAllAsync()
        {
            var equipments = await _equipmentRepository.GetAllAsync();

            return equipments
                .Select(e => new EquipmentDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    SerialNumber = e.SerialNumber,
                    Location = e.Location,
                    Status = e.Status
                })
                .ToList();
        }

        public async Task<EquipmentDto?> GetByIdAsync(int id)
        {
            var equipment = await _equipmentRepository.GetByIdAsync(id);

            if (equipment == null)
            {
                return null;
            }

            return new EquipmentDto
            {
                Id = equipment.Id,
                Name = equipment.Name,
                SerialNumber = equipment.SerialNumber,
                Location = equipment.Location,
                Status = equipment.Status
            };
        }

        public async Task<EquipmentDto> CreateAsync(CreateEquipmentDto dto)
        {
            var equipment = new Equipment
            {
                Name = dto.Name,
                SerialNumber = dto.SerialNumber,
                Location = dto.Location,
                Status = dto.Status
            };

            await _equipmentRepository.AddAsync(equipment);

            await _notificationService.CreateAsync(
                $"Equipment '{equipment.Name}' created.");

            return new EquipmentDto
            {
                Id = equipment.Id,
                Name = equipment.Name,
                SerialNumber = equipment.SerialNumber,
                Location = equipment.Location,
                Status = equipment.Status
            };
        }

        public async Task UpdateAsync(int id, UpdateEquipmentDto dto)
        {
            var equipment = await _equipmentRepository.GetByIdAsync(id);

            if (equipment == null)
            {
                throw new Exception("Equipment not found");
            }

            equipment.Name = dto.Name;
            equipment.SerialNumber = dto.SerialNumber;
            equipment.Location = dto.Location;
            equipment.Status = dto.Status;

            await _equipmentRepository.UpdateAsync(equipment);

            // Notification désactivée temporairement pour éviter l'erreur 500.
        }

        public async Task DeleteAsync(int id)
        {
            var equipment = await _equipmentRepository.GetByIdAsync(id);

            if (equipment == null)
            {
                throw new Exception("Equipment not found");
            }

            await _equipmentRepository.DeleteAsync(equipment);

            await _notificationService.CreateAsync(
                $"Equipment '{equipment.Name}' deleted.");
        }
    }
}