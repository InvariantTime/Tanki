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
        private readonly IHashService _hasher;

        public SessionService(IRoomService rooms, IUserRepository users, IHashService hasher)
        {
            _rooms = rooms;
            _users = users;
            _hasher = hasher;
        }

        public async Task<Result<Guid>> CreateSession(SessionCreationInfo info)
        {
            var password = info.Password == string.Empty ? string.Empty 
                : _hasher.CreateHash(info.Password);

            var room = new Room
            {
                HostId = info.UserId,
                MaxPlayerCount = info.MaxPlayerCount,
                Name = info.Name,
                PasswordHash = password
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

        public Result<Guid> GetJoinPermission(Guid roomId, string password)
        {
            var session = _sessions.FirstOrDefault(x => x.Value.Room.Id == roomId).Value;

            if (session == null)
                return Result.Failure<Guid>("Invalid room id");

            if (session.Users.Count >= session.Room.MaxPlayerCount)
                return Result.Failure<Guid>("Room is full");

            var passwordHash = session.Room.PasswordHash;

            if (passwordHash != string.Empty && _hasher.Compare(passwordHash, password) == false)
                return Result.Failure<Guid>("Incorrect password");

            return Result.Success(session.Id);
        }
    }
}