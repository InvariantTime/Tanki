using System.Collections.Concurrent;
using Tanki.Domain;
using Tanki.Domain.Models;
using Tanki.Domain.Repositories;

namespace Tanki.Services
{
    public class SessionService : ISessionService
    {
        private static readonly ConcurrentDictionary<Guid, GameSession> _sessions = new();

        private readonly IRoomRepository _rooms;

        public SessionService(IRoomRepository rooms)
        {
            _rooms = rooms;
        }

        public async Task<Result<Guid>> CreateSession(SessionCreationInfo creation)
        {
            var room = new Room()
            {
                HostId = creation.UserId,
                MaxPlayerCount = creation.MaxPlayerCount,
                Name = creation.Name,
                PasswordHash = string.Empty// TODO: Password
            };

            await _rooms.Add(room);

            var session = new GameSession(room);
            bool _ = _sessions.TryAdd(session.Id, session);

            return Result.Success(session.Id);
        }

        public Result<GameSession> Get(Guid id)
        {
            var result = _sessions.TryGetValue(id, out var session);

            if (result == false)
                return Result.Failure<GameSession>("There is no such session");

            return Result.Success(session!);
        }

        public IEnumerable<GameSession> GetAll()
        {
            return _sessions.Values;
        }

        public Task<Result> JoinToSession(User user, Guid id)
        {
            var result = Get(id);

            if (result.IsSuccess == false)
                return Task.FromResult(Result.Failure(result.Error));

            var session = result.Value!;

            if (session.AddUser(user) == false)
                return Task.FromResult(Result.Failure(string.Empty));//TODO: Error?

            return Task.FromResult(Result.Success());
        }

        public Task<Result> LeaveSession(User user, Guid session)
        {
            return Task.FromResult(Result.Failure(string.Empty));
        }
    }
}
