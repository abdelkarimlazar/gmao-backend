using GmaoIntentApi.Models;
using GmaoIntentApi.Repositories.Interfaces;
using GmaoIntentApi.Services.Interfaces;

namespace GmaoIntentApi.Services
{
    public class NotificationService
        : INotificationService
    {
        private readonly INotificationRepository
            _notificationRepository;

        public NotificationService(
            INotificationRepository
                notificationRepository)
        {
            _notificationRepository =
                notificationRepository;
        }

        public async Task<
            IReadOnlyCollection<Notification>>
            GetAllAsync()
        {
            return await
                _notificationRepository
                    .GetAllAsync();
        }

        public async Task CreateAsync(
            string message)
        {
            var notification =
                new Notification
                {
                    Message = message
                };

            await _notificationRepository
                .AddAsync(notification);
        }

        public async Task MarkAsReadAsync(
            int id)
        {
            var notification =
                await _notificationRepository
                    .GetByIdAsync(id);

            if (notification == null)
            {
                throw new Exception(
                    "Notification not found");
            }

            notification.ReadAt =
                DateTime.UtcNow;

            await _notificationRepository
                .MarkAsReadAsync(
                    notification);
        }
    }
}