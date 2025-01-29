using Microsoft.AspNetCore.Http;
using Tanki.Domain;
using Tanki.Domain.Models;

namespace Tanki.Infrastructure
{
    public interface IAccountService
    {
        Task<Result<string>> SignIn(string name, string password);

        Task<Result<string>> Register(string name, string password);

        Task<Result<User>> GetUser(HttpContext context);
    }
}
