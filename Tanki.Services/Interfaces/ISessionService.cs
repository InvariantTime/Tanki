using Tanki.Domain;
using Tanki.Domain.Models;

namespace Tanki.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<GameSession> GetByPage(int index = 1, int pageSize = 1);

        int GetCount();

        Result<GameSession> GetById(Guid id);

        Task<Result<GameSession>> Create(SessionCreationInfo info);

        Result Remove(Guid id);

        Result<string> Access(Guid session, string password);
    }
}