using Tanki.Domain.Models;

namespace Tanki.Domain.Repositories
{
    public interface ISessionRepository
    {
        Result<GameSession> Get(Guid id);

        int GetCount();

        IEnumerable<GameSession> GetByPage(int index = 1, int pageSize = 1);

        Result Add(GameSession session);
    }
}
