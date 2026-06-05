using GmaoIntentApi.Models;

namespace GmaoIntentApi.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Task<IReadOnlyCollection<Notification>>
            GetAllAsync();

        Task<Notification?> GetByIdAsync(
            int id);

        Task AddAsync(
            Notification notification);

        Task MarkAsReadAsync(
            Notification notification);
    }
}