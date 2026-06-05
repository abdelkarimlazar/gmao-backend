using GmaoIntentApi.Data;
using GmaoIntentApi.Models;
using GmaoIntentApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GmaoIntentApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(IReadOnlyCollection<User> Items, int TotalCount)> GetPagedAsync(string? search, int pageNumber, int pageSize)
        {
            var query = _context.Users.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(user => user.FullName.Contains(search) || user.Email.Contains(search));
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(user => user.FullName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<IReadOnlyCollection<User>> GetAllAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .OrderBy(user => user.FullName)
                .ToListAsync();
        }

        public Task<User?> GetByIdAsync(int id)
        {
            return _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            return _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
