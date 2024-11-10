using Tanki.Domain;
using Tanki.Domain.Models;

namespace Tanki.Services.Interfaces
{
    public interface ISessionService
    {
        Task<Result<Guid>> CreateSession(SessionCreationInfo info);

        Task<Result<GameSession>> Join(Guid id, Guid user);

        Task<SessionLeaveStates> Leave(Guid id, Guid user);

        IEnumerable<GameSession> GetAll();

        GameSession? Get(Guid id);
    }

    public enum SessionLeaveStates
    {
        Failure,
        Success,
        IsHost,
    }
}