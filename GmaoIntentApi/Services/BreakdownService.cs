using GmaoIntentApi.DTOs;
using GmaoIntentApi.Models;
using GmaoIntentApi.Repositories.Interfaces;
using GmaoIntentApi.Services.Interfaces;

namespace GmaoIntentApi.Services
{
    public class BreakdownService
        : IBreakdownService
    {
        private readonly IBreakdownRepository
            _breakdownRepository;

        private readonly IEquipmentRepository
            _equipmentRepository;

        private readonly IUserRepository
            _userRepository;

        private readonly INotificationService
            _notificationService;

        public BreakdownService(
            IBreakdownRepository breakdownRepository,
            IEquipmentRepository equipmentRepository,
            IUserRepository userRepository,
            INotificationService notificationService)
        {
            _breakdownRepository =
                breakdownRepository;

            _equipmentRepository =
                equipmentRepository;

            _userRepository =
                userRepository;

            _notificationService =
                notificationService;
        }

        public async Task<
            IReadOnlyCollection<BreakdownDto>>
            GetBreakdownsAsync()
        {
            var breakdowns =
                await _breakdownRepository
                    .GetAllAsync();

            return breakdowns
                .Select(Map)
                .ToArray();
        }

        public async Task<BreakdownDto?>
            GetBreakdownByIdAsync(int id)
        {
            var breakdown =
                await _breakdownRepository
                    .GetByIdAsync(id);

            return breakdown is null
                ? null
                : Map(breakdown);
        }

        public async Task<BreakdownDto?>
            CreateBreakdownAsync(
                CreateBreakdownDto dto)
        {
            if (
                !await _equipmentRepository
                    .ExistsAsync(dto.EquipmentId)

                ||

                await _userRepository
                    .GetByIdAsync(dto.ReportedBy)
                    is null
            )
            {
                return null;
            }

            var breakdown = new Breakdown
            {
                EquipmentId =
                    dto.EquipmentId,

                ReportedBy =
                    dto.ReportedBy,

                Description =
                    dto.Description,

                Date =
                    dto.Date,

                Status =
                    dto.Status
            };

            await _breakdownRepository
                .AddAsync(breakdown);

            // NOTIFICATION

            await _notificationService
                .CreateAsync(
                    "New breakdown reported.");

            var createdBreakdown =
                await _breakdownRepository
                    .GetByIdAsync(breakdown.Id);

            return createdBreakdown is null
                ? null
                : Map(createdBreakdown);
        }

        public async Task<bool?>
            UpdateBreakdownAsync(
                int id,
                UpdateBreakdownDto dto)
        {
            var breakdown =
                await _breakdownRepository
                    .GetByIdAsync(id);

            if (breakdown is null)
            {
                return false;
            }

            if (
                !await _equipmentRepository
                    .ExistsAsync(dto.EquipmentId)

                ||

                await _userRepository
                    .GetByIdAsync(dto.ReportedBy)
                    is null
            )
            {
                return null;
            }

            breakdown.EquipmentId =
                dto.EquipmentId;

            breakdown.ReportedBy =
                dto.ReportedBy;

            breakdown.Description =
                dto.Description;

            breakdown.Date =
                dto.Date;

            breakdown.Status =
                dto.Status;

            await _breakdownRepository
                .UpdateAsync(breakdown);

            return true;
        }

        public async Task<bool>
            DeleteBreakdownAsync(int id)
        {
            var breakdown =
                await _breakdownRepository
                    .GetByIdAsync(id);

            if (breakdown is null)
            {
                return false;
            }

            await _breakdownRepository
                .DeleteAsync(breakdown);

            return true;
        }

        private static BreakdownDto
            Map(Breakdown breakdown)
        {
            return new BreakdownDto
            {
                Id =
                    breakdown.Id,

                EquipmentId =
                    breakdown.EquipmentId,

                EquipmentName =
                    breakdown.Equipment.Name,

                ReportedBy =
                    breakdown.ReportedBy,

                ReportedByName =
                    breakdown.User.FullName,

                Description =
                    breakdown.Description,

                Date =
                    breakdown.Date,

                Status =
                    breakdown.Status
            };
        }
    }
}