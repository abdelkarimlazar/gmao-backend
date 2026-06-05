using GmaoIntentApi.Data;
using GmaoIntentApi.Models;
using GmaoIntentApi.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace GmaoIntentApi.Repositories
{
    public class NotificationRepository
        : INotificationRepository
    {
        private readonly AppDbContext
            _context;

        public NotificationRepository(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<
            IReadOnlyCollection<Notification>>
            GetAllAsync()
        {
            return await _context.Notifications
                .OrderByDescending(
                    n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<Notification?>
            GetByIdAsync(int id)
        {
            return await _context.Notifications
                .FirstOrDefaultAsync(
                    n => n.Id == id);
        }

        public async Task AddAsync(
            Notification notification)
        {
            await _context.Notifications
                .AddAsync(notification);

            await _context.SaveChangesAsync();
        }

        public async Task MarkAsReadAsync(
            Notification notification)
        {
            _context.Notifications
                .Update(notification);

            await _context.SaveChangesAsync();
        }
    }
}