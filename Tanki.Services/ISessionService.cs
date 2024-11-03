using Tanki.Domain;
using Tanki.Domain.Models;

namespace Tanki.Services
{
    public interface ISessionService
    {
        Task<Result<Guid>> CreateSession(SessionCreationInfo room);

        Task<Result> JoinToSession(User user, Guid session);

        Task<Result> LeaveSession(User user, Guid session);

        IEnumerable<GameSession> GetAll();

        Result<GameSession> Get(Guid id);
    }
}
