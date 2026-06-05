using GmaoIntentApi.Data;
using GmaoIntentApi.Models;
using GmaoIntentApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using GmaoTaskStatus = GmaoIntentApi.Enums.TaskStatus;

namespace GmaoIntentApi.Repositories
{
    public class MaintenanceTaskRepository : IMaintenanceTaskRepository
    {
        private readonly AppDbContext _context;

        public MaintenanceTaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(IReadOnlyCollection<MaintenanceTask> Items, int TotalCount)> GetPagedAsync(GmaoTaskStatus? status, DateTime? startDateFrom, DateTime? startDateTo, int pageNumber, int pageSize)
        {
            var query = _context.MaintenanceTasks
                .AsNoTracking()
                .Include(task => task.Equipment)
                .Include(task => task.User)
                .AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(task => task.Status == status.Value);
            }

            if (startDateFrom.HasValue)
            {
                query = query.Where(task => task.StartDate >= startDateFrom.Value);
            }

            if (startDateTo.HasValue)
            {
                query = query.Where(task => task.StartDate <= startDateTo.Value);
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(task => task.StartDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<IReadOnlyCollection<MaintenanceTask>> GetAllAsync()
        {
            return await _context.MaintenanceTasks
                .AsNoTracking()
                .Include(task => task.Equipment)
                .Include(task => task.User)
                .OrderBy(task => task.StartDate)
                .ToListAsync();
        }

        public Task<MaintenanceTask?> GetByIdAsync(int id)
        {
            return _context.MaintenanceTasks
                .Include(task => task.Equipment)
                .Include(task => task.User)
                .FirstOrDefaultAsync(task => task.Id == id);
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _context.MaintenanceTasks.AnyAsync(task => task.Id == id);
        }

        public async Task<MaintenanceTask> AddAsync(MaintenanceTask task)
        {
            _context.MaintenanceTasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task UpdateAsync(MaintenanceTask task)
        {
            _context.MaintenanceTasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(MaintenanceTask task)
        {
            _context.MaintenanceTasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
