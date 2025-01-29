using System.Collections.Concurrent;
using Tanki.Domain.Models;

namespace Tanki.Domain.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private static readonly ConcurrentDictionary<Guid, GameSession> _sessions = new();

        public Result<GameSession> Get(Guid id)
        {
            if (_sessions.ContainsKey(id) == false)
                return Result.Failure<GameSession>("Unknown session");

            return Result.Success(_sessions[id]);
        }

        public IEnumerable<GameSession> GetByPage(int index = 1, int pageSize = 1)
        {
            return _sessions.Values.Skip(pageSize * (index - 1))
                .Take(pageSize);
        }

        public int GetCount()
        {
            return _sessions.Count;
        }

        public Result Add(GameSession session)
        {
            if (_sessions.FirstOrDefault(x => x.Value.Name == session.Name).Value != null)
                return Result.Failure("There is already session with such name");

            bool result = _sessions.TryAdd(session.Id, session);

            if (result == false)
                return Result.Failure("Unable to add session");

            return Result.Success();
        }

        public Result Remove(Guid id)
        {
            var result = _sessions.TryRemove(id, out _);

            if (result == true)
                return Result.Success();

            return Result.Failure("Unable to remove session");
        }
    }
}
