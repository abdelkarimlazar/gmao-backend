using GmaoIntentApi.Data;
using GmaoIntentApi.Enums;
using GmaoIntentApi.Models;
using GmaoIntentApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GmaoIntentApi.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly AppDbContext _context;

        public EquipmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(IReadOnlyCollection<Equipment> Items, int TotalCount)>
            GetPagedAsync(
                EquipmentStatus? status,
                int pageNumber,
                int pageSize)
        {
            var query = _context.Equipments.AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(e =>
                    e.Status == status.Value);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<IReadOnlyCollection<Equipment>> GetAllAsync()
        {
            return await _context.Equipments.ToListAsync();
        }

        public async Task<Equipment?> GetByIdAsync(int id)
        {
            return await _context.Equipments
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Equipments
                .AnyAsync(e => e.Id == id);
        }

        public async Task<Equipment> AddAsync(Equipment equipment)
        {
            _context.Equipments.Add(equipment);

            await _context.SaveChangesAsync();

            return equipment;
        }

        public async Task UpdateAsync(Equipment equipment)
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Equipment equipment)
        {
            _context.Equipments.Remove(equipment);

            await _context.SaveChangesAsync();
        }
    }
}