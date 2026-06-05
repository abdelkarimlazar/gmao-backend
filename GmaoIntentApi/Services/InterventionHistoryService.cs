using GmaoIntentApi.DTOs;
using GmaoIntentApi.Models;
using GmaoIntentApi.Repositories.Interfaces;
using GmaoIntentApi.Services.Interfaces;

namespace GmaoIntentApi.Services
{
    public class InterventionHistoryService : IInterventionHistoryService
    {
        private readonly IInterventionHistoryRepository _interventionHistoryRepository;
        private readonly IMaintenanceTaskRepository _maintenanceTaskRepository;
        private readonly IUserRepository _userRepository;

        public InterventionHistoryService(IInterventionHistoryRepository interventionHistoryRepository, IMaintenanceTaskRepository maintenanceTaskRepository, IUserRepository userRepository)
        {
            _interventionHistoryRepository = interventionHistoryRepository;
            _maintenanceTaskRepository = maintenanceTaskRepository;
            _userRepository = userRepository;
        }

        public async Task<IReadOnlyCollection<InterventionHistoryDto>> GetInterventionHistoriesAsync()
        {
            var histories = await _interventionHistoryRepository.GetAllAsync();
            return histories.Select(Map).ToArray();
        }

        public async Task<IReadOnlyCollection<InterventionHistoryDto>> GetInterventionHistoriesByTaskAsync(int taskId)
        {
            var histories = await _interventionHistoryRepository.GetByTaskIdAsync(taskId);
            return histories.Select(Map).ToArray();
        }

        public async Task<InterventionHistoryDto?> GetInterventionHistoryByIdAsync(int id)
        {
            var history = await _interventionHistoryRepository.GetByIdAsync(id);
            return history is null ? null : Map(history);
        }

        public async Task<InterventionHistoryDto?> CreateInterventionHistoryAsync(CreateInterventionHistoryDto dto)
        {
            if (!await _maintenanceTaskRepository.ExistsAsync(dto.TaskId) || await _userRepository.GetByIdAsync(dto.UserId) is null)
            {
                return null;
            }

            var history = new InterventionHistory
            {
                TaskId = dto.TaskId,
                UserId = dto.UserId,
                Action = dto.Action,
                Date = dto.Date,
                Comment = dto.Comment
            };

            await _interventionHistoryRepository.AddAsync(history);
            var createdHistory = await _interventionHistoryRepository.GetByIdAsync(history.Id);
            return createdHistory is null ? null : Map(createdHistory);
        }

        public async Task<bool?> UpdateInterventionHistoryAsync(int id, UpdateInterventionHistoryDto dto)
        {
            var history = await _interventionHistoryRepository.GetByIdAsync(id);
            if (history is null)
            {
                return false;
            }

            if (!await _maintenanceTaskRepository.ExistsAsync(dto.TaskId) || await _userRepository.GetByIdAsync(dto.UserId) is null)
            {
                return null;
            }

            history.TaskId = dto.TaskId;
            history.UserId = dto.UserId;
            history.Action = dto.Action;
            history.Date = dto.Date;
            history.Comment = dto.Comment;

            await _interventionHistoryRepository.UpdateAsync(history);
            return true;
        }

        public async Task<bool> DeleteInterventionHistoryAsync(int id)
        {
            var history = await _interventionHistoryRepository.GetByIdAsync(id);
            if (history is null)
            {
                return false;
            }

            await _interventionHistoryRepository.DeleteAsync(history);
            return true;
        }

        private static InterventionHistoryDto Map(InterventionHistory history)
        {
            return new InterventionHistoryDto
            {
                Id = history.Id,
                TaskId = history.TaskId,
                TaskTitle = history.Task.Title,
                UserId = history.UserId,
                UserName = history.User.FullName,
                Action = history.Action,
                Date = history.Date,
                Comment = history.Comment
            };
        }
    }
}
