using Tanki.Domain;
using Tanki.Domain.Models;

namespace Tanki.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result<User>> CreateUser(string name, string password);

        Task<Result<User>> GetUser(Guid id);

        Task<Result<User>> GetUser(string name, string password);
    }
}