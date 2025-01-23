using Microsoft.EntityFrameworkCore;
using Tanki.Domain.Models;
using Tanki.Domain.Repositories;

namespace Tanki.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ContainsWithName(string name)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Name == name);

            return user != null;
        }

        public async Task<User?> GetUserByName(string name)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Name == name);

            return user;
        }

        public async Task RemoveUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUser(Guid id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}