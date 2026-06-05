using GmaoIntentApi.DTOs;
using GmaoIntentApi.Models;
using GmaoIntentApi.Repositories.Interfaces;
using GmaoIntentApi.Services.Interfaces;

namespace GmaoIntentApi.Services
{
    public class MaintenanceTaskService
        : IMaintenanceTaskService
    {
        private readonly IMaintenanceTaskRepository
            _maintenanceTaskRepository;

        private readonly IEquipmentRepository
            _equipmentRepository;

        private readonly IUserRepository
            _userRepository;

        private readonly INotificationService
            _notificationService;

        public MaintenanceTaskService(
            IMaintenanceTaskRepository maintenanceTaskRepository,
            IEquipmentRepository equipmentRepository,
            IUserRepository userRepository,
            INotificationService notificationService)
        {
            _maintenanceTaskRepository =
                maintenanceTaskRepository;

            _equipmentRepository =
                equipmentRepository;

            _userRepository =
                userRepository;

            _notificationService =
                notificationService;
        }

        public async Task<
            PagedResultDto<MaintenanceTaskDto>>
            GetMaintenanceTasksAsync(
                MaintenanceTaskQueryDto query)
        {
            var (items, totalCount) =
                await _maintenanceTaskRepository
                    .GetPagedAsync(
                        query.Status,
                        query.StartDateFrom,
                        query.StartDateTo,
                        query.PageNumber,
                        query.PageSize);

            return new PagedResultDto<
                MaintenanceTaskDto>
            {
                Items = items
                    .Select(Map)
                    .ToArray(),

                PageNumber =
                    query.PageNumber,

                PageSize =
                    query.PageSize,

                TotalCount =
                    totalCount,

                TotalPages =
                    (int)Math.Ceiling(
                        totalCount /
                        (double)query.PageSize)
            };
        }

        public async Task<MaintenanceTaskDto?>
            GetMaintenanceTaskByIdAsync(int id)
        {
            var task =
                await _maintenanceTaskRepository
                    .GetByIdAsync(id);

            return task is null
                ? null
                : Map(task);
        }

        public async Task<MaintenanceTaskDto?>
            CreateMaintenanceTaskAsync(
                CreateMaintenanceTaskDto dto)
        {
            if (
                !await _equipmentRepository
                    .ExistsAsync(dto.EquipmentId)

                ||

                await _userRepository
                    .GetByIdAsync(dto.AssignedTo)
                    is null
            )
            {
                return null;
            }

            var task = new MaintenanceTask
            {
                Title = dto.Title,

                Description =
                    dto.Description,

                EquipmentId =
                    dto.EquipmentId,

                AssignedTo =
                    dto.AssignedTo,

                StartDate =
                    dto.StartDate,

                EndDate =
                    dto.EndDate,

                Status =
                    dto.Status,

                Priority =
                    dto.Priority
            };

            await _maintenanceTaskRepository
                .AddAsync(task);

            // NOTIFICATION

            await _notificationService
                .CreateAsync(
                    "New maintenance task created.");

            var createdTask =
                await _maintenanceTaskRepository
                    .GetByIdAsync(task.Id);

            return createdTask is null
                ? null
                : Map(createdTask);
        }

        public async Task<bool?>
            UpdateMaintenanceTaskAsync(
                int id,
                UpdateMaintenanceTaskDto dto)
        {
            var task =
                await _maintenanceTaskRepository
                    .GetByIdAsync(id);

            if (task is null)
            {
                return false;
            }

            if (
                !await _equipmentRepository
                    .ExistsAsync(dto.EquipmentId)

                ||

                await _userRepository
                    .GetByIdAsync(dto.AssignedTo)
                    is null
            )
            {
                return null;
            }

            task.Title =
                dto.Title;

            task.Description =
                dto.Description;

            task.EquipmentId =
                dto.EquipmentId;

            task.AssignedTo =
                dto.AssignedTo;

            task.StartDate =
                dto.StartDate;

            task.EndDate =
                dto.EndDate;

            task.Status =
                dto.Status;

            task.Priority =
                dto.Priority;

            await _maintenanceTaskRepository
                .UpdateAsync(task);

            return true;
        }

        public async Task<bool>
            DeleteMaintenanceTaskAsync(int id)
        {
            var task =
                await _maintenanceTaskRepository
                    .GetByIdAsync(id);

            if (task is null)
            {
                return false;
            }

            await _maintenanceTaskRepository
                .DeleteAsync(task);

            return true;
        }

        private static MaintenanceTaskDto
            Map(MaintenanceTask task)
        {
            return new MaintenanceTaskDto
            {
                Id = task.Id,

                Title = task.Title,

                Description =
                    task.Description,

                EquipmentId =
                    task.EquipmentId,

                EquipmentName =
                    task.Equipment.Name,

                AssignedTo =
                    task.AssignedTo,

                AssignedUserName =
                    task.User.FullName,

                StartDate =
                    task.StartDate,

                EndDate =
                    task.EndDate,

                Status =
                    task.Status,

                Priority =
                    task.Priority
            };
        }
    }
}