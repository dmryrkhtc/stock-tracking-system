using STS.Domain.Entities;
using STS.Application.IRepositories;
using STS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace STS.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly STSDbContext _context;

        public UserRepository(STSDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
