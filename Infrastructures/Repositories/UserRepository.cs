using Application.Abstractions;
using Domain.Entities;
using Infrastructures.DBaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories
{

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<(List<User>, int)> GetAllPaginatedAsync(int page, int size)
        {
            var query = _context.Users.AsQueryable();

            var count = await query.CountAsync();

            var users = await query
                .OrderBy(u => u.FullName)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return (users, count);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(a => a.Email == email);
        }
    }

}
