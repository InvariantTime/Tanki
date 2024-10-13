using Tanki.Domain.Models;

namespace Tanki.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUser(Guid id);

        Task<User?> GetUserByName(string name);

        Task<bool> ContainsWithName(string name);

        Task AddUser(User user);

        Task RemoveUser(User user);

        Task UpdateUser(User user);
    }
}