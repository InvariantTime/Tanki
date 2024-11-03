using Microsoft.AspNetCore.SignalR;
using Tanki.Responces;
using Tanki.Services;

namespace Tanki.Hubs
{
    public interface ISessionClient
    {
        Task PlayersChanged(IEnumerable<UserScoreResponce> users);
    }

    public class SessionHub : Hub<ISessionClient>
    {
        private const string _sessionQuery = "sessionId";

        private readonly ISessionService _sessions;
        private readonly IUserService _users;

        public SessionHub(ISessionService sessions, IUserService users)
        {
            _sessions = sessions;
            _users = users;
        }

        public async Task Join()
        {
            var sessionId = Context.GetHttpContext()!.Request.Query[_sessionQuery];
            var userId = Context.GetHttpContext()!.Session.Get(SessionOptions.SessionUserId);

            if (userId == null)
                return;

            var user = await _users.GetUser(new Guid(userId));

            if (user.IsSuccess == false)
                return;

            if (Guid.TryParse(sessionId, out var id) == true && id != Guid.Empty)
            {
                var result = await _sessions.JoinToSession(user.Value!, id);
                var session = _sessions.Get(id);

                if (result.IsSuccess == true && session.IsSuccess == true)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, sessionId!);

                    await Clients.All.PlayersChanged(session.Value!.Users
                        .Select(x => new UserScoreResponce(x.Name, x.Score)));
                }
            }
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
