using Tanki.Domain;
using Tanki.Domain.Models;
using Tanki.Domain.Repositories;
using Tanki.Services.Interfaces;

namespace Tanki.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessions;
        private readonly IHashService _hasher;
        private readonly IUserService _users;

        public SessionService(ISessionRepository sessions, 
            IUserService users, IHashService hasher)
        {
            _sessions = sessions;
            _users = users;
            _hasher = hasher;
        }

        public IEnumerable<GameSession> GetByPage(int index = 1, int pageSize = 1)
        {
            return _sessions.GetByPage(index, pageSize);
        }
        
        public int GetCount()
        {
            return _sessions.GetCount();
        }

        public async Task<Result<GameSession>> Create(SessionCreationInfo info)
        {
            var password = string.IsNullOrEmpty(info.Password) == false 
                ? _hasher.CreateHash(info.Password) : string.Empty;

            var user = await _users.GetUser(info.UserId);

            if (user.IsSuccess == false)
                return Result.Failure<GameSession>(user.Error);

            var scene = new SessionScene(user.Value!, info.MaxPlayerCount);

            var session = GameSession.Create(info.Name, scene, password);
            var result = _sessions.Add(session);

            if (result.IsSuccess == false)
                return Result.Failure<GameSession>(result.Error);

            return Result.Success(session);
        }

        public Result<GameSession> GetById(Guid id)
        {
            return _sessions.Get(id);
        }
    }
}
