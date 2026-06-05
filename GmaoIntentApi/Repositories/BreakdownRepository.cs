using GmaoIntentApi.Data;
using GmaoIntentApi.Models;
using GmaoIntentApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GmaoIntentApi.Repositories
{
    public class BreakdownRepository : IBreakdownRepository
    {
        private readonly AppDbContext _context;

        public BreakdownRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Breakdown>> GetAllAsync()
        {
            return await _context.Breakdowns
                .AsNoTracking()
                .Include(breakdown => breakdown.Equipment)
                .Include(breakdown => breakdown.User)
                .OrderByDescending(breakdown => breakdown.Date)
                .ToListAsync();
        }

        public Task<Breakdown?> GetByIdAsync(int id)
        {
            return _context.Breakdowns
                .Include(breakdown => breakdown.Equipment)
                .Include(breakdown => breakdown.User)
                .FirstOrDefaultAsync(breakdown => breakdown.Id == id);
        }

        public async Task<Breakdown> AddAsync(Breakdown breakdown)
        {
            _context.Breakdowns.Add(breakdown);
            await _context.SaveChangesAsync();
            return breakdown;
        }

        public async Task UpdateAsync(Breakdown breakdown)
        {
            _context.Breakdowns.Update(breakdown);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Breakdown breakdown)
        {
            _context.Breakdowns.Remove(breakdown);
            await _context.SaveChangesAsync();
        }
    }
}
