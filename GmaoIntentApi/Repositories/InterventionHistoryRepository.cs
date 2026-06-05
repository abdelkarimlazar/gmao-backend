using GmaoIntentApi.Data;
using GmaoIntentApi.Models;
using GmaoIntentApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GmaoIntentApi.Repositories
{
    public class InterventionHistoryRepository : IInterventionHistoryRepository
    {
        private readonly AppDbContext _context;

        public InterventionHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<InterventionHistory>> GetAllAsync()
        {
            return await _context.InterventionHistories
                .AsNoTracking()
                .Include(history => history.Task)
                .Include(history => history.User)
                .OrderByDescending(history => history.Date)
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<InterventionHistory>> GetByTaskIdAsync(int taskId)
        {
            return await _context.InterventionHistories
                .AsNoTracking()
                .Include(history => history.Task)
                .Include(history => history.User)
                .Where(history => history.TaskId == taskId)
                .OrderByDescending(history => history.Date)
                .ToListAsync();
        }

        public Task<InterventionHistory?> GetByIdAsync(int id)
        {
            return _context.InterventionHistories
                .Include(history => history.Task)
                .Include(history => history.User)
                .FirstOrDefaultAsync(history => history.Id == id);
        }

        public async Task<InterventionHistory> AddAsync(InterventionHistory history)
        {
            _context.InterventionHistories.Add(history);
            await _context.SaveChangesAsync();
            return history;
        }

        public async Task UpdateAsync(InterventionHistory history)
        {
            _context.InterventionHistories.Update(history);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(InterventionHistory history)
        {
            _context.InterventionHistories.Remove(history);
            await _context.SaveChangesAsync();
        }
    }
}
