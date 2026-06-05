using GmaoIntentApi.Models;

namespace GmaoIntentApi.Services.Interfaces
{
    public interface INotificationService
    {
        Task<IReadOnlyCollection<Notification>>
            GetAllAsync();

        Task CreateAsync(
            string message);

        Task MarkAsReadAsync(
            int id);
    }
}