using System.Collections.Concurrent;
using Tanki.Domain;
using Tanki.Domain.Models;
using Tanki.Domain.Repositories;

namespace Tanki.Services.Interfaces
{
    public class SessionService : ISessionService
    {
        private static readonly ConcurrentDictionary<Guid, GameSession> _sessions = new();

        private readonly IRoomService _rooms;
        private readonly IUserRepository _users;

        public SessionService(IRoomService rooms, IUserRepository users)
        {
            _rooms = rooms;
            _users = users;
        }

        public async Task<Result<Guid>> CreateSession(SessionCreationInfo info)
        {
            var room = new Room
            {
                HostId = info.UserId,
                MaxPlayerCount = info.MaxPlayerCount,
                Name = info.Name,
            };

            var result = await _rooms.AddRoom(room);

            if (result.IsSuccess == false)
                return Result.Failure<Guid>(result.Error);

            var session = GameSession.Create(room);
            _sessions.TryAdd(session.Id, session);

            return Result.Success(session.Id);
        }

        public async Task<Result<GameSession>> Join(Guid id, Guid userId)
        {
            _sessions.TryGetValue(id, out var session);

            if (session == null)
                return Result.Failure<GameSession>("Id is invalid");

            var user = await _users.GetUser(userId);

            if (user == null)
                return Result.Failure<GameSession>("user is not authorized");

            var result = session.AddUser(user);

            if (result == false)
                return Result.Failure<GameSession>("Unable to connect");

            await _rooms.SetPlayerCount(session.Room.Id, (uint)session.Users.Count);
            return Result.Success(session);
        }

        public async Task<SessionLeaveStates> Leave(Guid id, Guid userId)
        {
            _sessions.TryGetValue(id, out var session);

            if (session == null)
                return SessionLeaveStates.Failure;

            bool result = session.RemoveUser(userId);

            if (result == false)
                return SessionLeaveStates.Failure;

            if (session.IsHost(userId) == true)
            {
                await _rooms.DeleteRoom(session.Room.Id);
                _sessions.Remove(session.Id, out _);

                return SessionLeaveStates.IsHost;
            }

            await _rooms.SetPlayerCount(session.Room.Id, (uint)session.Users.Count);
            return SessionLeaveStates.Success;
        }

        public IEnumerable<GameSession> GetAll()
        {
            return _sessions.Values;
        }

        public GameSession? Get(Guid id)
        {
            _sessions.TryGetValue(id, out var session);
            return session;
        }
    }
}